using System;
using System.Reflection;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using TXTCommunication.Fischertechnik;
using System.IO;
using System.Threading.Tasks;

namespace FtAiMsDemo.Helpers
{
    // You exclude the 'Extension' suffix when using in Xaml markup
    [Preserve(AllMembers = true)]
    public class FtCameraExtension : IMarkupExtension
    {


        public object ProvideValue(IServiceProvider serviceProvider)
        {            
             return new FtImageSource();
        }


        
    }    
    
        public class FtImageSource : StreamImageSource
        {
                private byte[] imageBytes { get; set;}
                private bool isDirty = false;

                public FtImageSource()
                {
                    InitFromResource();
                    App.TxtLink.Connected += TxtLink_Connected;

                    Func<Stream> streamFunc = () => {
                        lock (imageBytes)
                        {
                            isDirty = false;
                            return new MemoryStream(this.imageBytes);
                        }
                    };

                    this.Stream = token => Task.Run(streamFunc, token);
                }

                private void TxtLink_Connected(object sender, EventArgs e){
                    App.TxtLink.TxtCamera.StartCamera();
                    App.TxtLink.TxtCamera.FrameReceived += TxtCamera_FrameReceived;
                   this.Run();
                }

                private void Run()
                {
                     Device.StartTimer(TimeSpan.FromSeconds(1),() => {
                         this.OnSourceChanged();
                         return true;
                     });
                }

        private void  InitFromResource()
                {
                    var resource = "FtAiMsDemo.Resources.RoboProImage_0.jpg";
                    var assembly = typeof(FtCameraExtension).GetTypeInfo().Assembly;
                    using (var inputStream = assembly.GetManifestResourceStream(resource))
                        using (var outputStream = new MemoryStream())
                        {
                            inputStream.CopyTo(outputStream);
                            this.imageBytes = outputStream.ToArray();
                        }
                }

                //static int FileNum = 0;
                private void TxtCamera_FrameReceived(object sender, TXTCommunication.Fischertechnik.Txt.Camera.FrameReceivedEventArgs e)
                {       lock (imageBytes)
                        {
                            isDirty = true;
                            imageBytes = e.FrameData;
                        }
                }
    }
       
}

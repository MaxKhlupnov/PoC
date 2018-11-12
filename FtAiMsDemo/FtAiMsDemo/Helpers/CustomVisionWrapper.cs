using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction.Models;

namespace FtAiMsDemo.Helpers
{
    public static class CustomVisionWrapper
    {

        private const string SouthCentralUsEndpoint = "https://southcentralus.api.cognitive.microsoft.com/";
        private const string predictionKey = "";
        private static Guid projectId = new Guid("6899f933-157e-4175-a04b-69ffe6bb6b5d");
        private const double predictionThreshold = 0.25;

        private static bool busy = false;

        // Create a prediction endpoint, passing in obtained prediction key
        private static CustomVisionPredictionClient endpoint = new CustomVisionPredictionClient()
        {
            ApiKey = predictionKey,
            Endpoint = SouthCentralUsEndpoint
        };


        public static string PredictImage(Stream imageStream)
        {
            if (busy)
            {
                App.Log.WriteInfo("Skip image while robot is in process");
                return null;
            }
            App.Log.WriteInfo(String.Format("ComputerVision call. images size {0}",imageStream.Length));
            ImagePrediction results = endpoint.PredictImage(projectId, imageStream);
            PredictionModel model = results.Predictions.OrderByDescending<PredictionModel,double>(m => m.Probability).First<PredictionModel>();
            App.Log.WriteInfo(String.Format("ComputerVision recognize an images as {0} with probability {1}", model.TagName, model.Probability));
            
            if (model.Probability > predictionThreshold)
            {
                DoRobotAction(model.TagName);
                return model.TagName;
            }
            else
            {
                return null;
            }
        }

            public static void DoRobotAction(string actionType)
            {
                    try
                    {
                        busy = true;
                        if (actionType.Equals("Bananas", StringComparison.InvariantCultureIgnoreCase))
                        {
                            App.Log.WriteInfo("Performing Banana action");
                            FtCommandsWrapper.MouveToOnePosition();
                        }
                        else if (actionType.Equals("Orange", StringComparison.InvariantCultureIgnoreCase))
                        {
                            App.Log.WriteInfo("Performing Orange action");
                            FtCommandsWrapper.MouveToTwoPosition();
                        }
                        else if (actionType.Equals("Apple", StringComparison.InvariantCultureIgnoreCase))
                        {
                            App.Log.WriteInfo("Performing Apple action");
                            FtCommandsWrapper.MouveToThreePosition();
                        }
                        else
                        {
                            App.Log.WriteInfo(String.Format("Incorrect tag {0}", actionType));
                        }
                        }
                    catch (Exception ex)
                    {
                        App.Log.WriteError(ex);
                    }
                    finally
                    {
                        busy = false;
                    }
            }
        
    }
}

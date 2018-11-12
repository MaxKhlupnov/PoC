using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction.Models;

namespace PoCApp
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
                Console.WriteLine("Skip image while robot is in process");
                return null;
            }
            Console.WriteLine("ComputerVision call. images size {0}",imageStream.Length);
            ImagePrediction results = endpoint.PredictImage(projectId, imageStream);
            PredictionModel model = results.Predictions.OrderByDescending<PredictionModel,double>(m => m.Probability).First<PredictionModel>();
            Console.WriteLine("ComputerVision recognize an images as {0} with probability {1}", model.TagName, model.Probability);
            
            if (model.Probability > predictionThreshold)
            {
                DoRobotAction(model.TagName);
                return model.TagName;
            }
            else
            {
                Console.WriteLine("Ignore unknown item");
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
                            Console.WriteLine("Performing Banana action.");
                           // Console.ReadKey();
                            Program.MouveToOnePosition();
                        }
                        else if (actionType.Equals("Orange", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Console.WriteLine("Performing Orange action.");
                           // Console.ReadKey();
                            Program.MouveToTwoPosition();
                        }
                        else if (actionType.Equals("Apple", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Console.WriteLine("Performing Apple action.");
                           // Console.ReadKey();
                            Program.MouveToThreePosition();
                        }
                        else
                        {
                            Console.WriteLine(String.Format("Incorrect tag {0}", actionType));
                        }
                        }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    finally
                    {
                        busy = false;
                    }
            }
        
    }
}

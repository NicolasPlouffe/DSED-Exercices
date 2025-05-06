using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;

namespace appone
{
    class Response
    {
        public bool success { get; set; }
        public DetectedObject[] predictions { get; set; }
    }

    class DetectedObject
    {
        public string label { get; set; }
        public float confidence { get; set; }
        public int y_min { get; set; }
        public int x_min { get; set; }
        public int y_max { get; set; }
        public int x_max { get; set; }
    }

    class App
    {
        static HttpClient client = new HttpClient();

        public static string detectFaceJson(string image_path)
        {
            MultipartFormDataContent request = new MultipartFormDataContent();
            FileStream image_data = File.OpenRead(image_path);
            request.Add(new StreamContent(image_data), "image", Path.GetFileName(image_path));
            request.Add(new StringContent("Mysecretkey"), "api_key");
            Task<HttpResponseMessage> outputTask = client.PostAsync("http://localhost:32168/v1/vision/face", request);
            outputTask.Wait();
            HttpResponseMessage output = outputTask.Result;
            Task<string> jsonStringTask = output.Content.ReadAsStringAsync();
            jsonStringTask.Wait();
            string jsonString = jsonStringTask.Result;
            return jsonString;
        }

        static void Main(string[] args)
        {
            string image_path = "/home/nico/Pictures/latin.jpg";
            string output_dir = "/home/nico/Pictures/";

            // 1. Appel API et récupération JSON
            string jsonString = detectFaceJson(image_path);
            Console.WriteLine(jsonString);

            // 2. Désérialisation JSON en objet Response
            Response response = JsonSerializer.Deserialize<Response>(jsonString);

            if (response != null && response.success && response.predictions != null && response.predictions.Length > 0)
            {
                // 3. Chargement de l'image avec ImageSharp
                using (var img = Image.Load(image_path))
                {
                    // 4. Pour chaque objet détecté, dessiner un rectangle rouge
                    foreach (var rectangle in response.predictions)
                    {
                        var rect = new RectangleF(
                            rectangle.x_min,
                            rectangle.y_min,
                            rectangle.x_max - rectangle.x_min,
                            rectangle.y_max - rectangle.y_min);

                        img.Mutate(ctx => ctx.Draw(Color.Red, 2.0f, rect));
                    }

                    // 5. Création du dossier output s'il n'existe pas
                    if (!Directory.Exists(output_dir))
                    {
                        Directory.CreateDirectory(output_dir);
                    }

                    // 6. Sauvegarde de l'image modifiée dans le dossier output
                    string output_path = Path.Combine(output_dir, Path.GetFileName(image_path));
                    img.Save(output_path);

                    Console.WriteLine($"Image modifiée sauvegardée sous : {output_path}");
                }
            }
            else
            {
                Console.WriteLine("Aucun visage détecté ou échec de la détection.");
            }
        }
    }
}

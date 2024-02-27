using EFTWORKER_ICG.Models;
using EFTWORKER_ICG.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;

namespace EFTWORKER_ICG.Services
{
    public class FirebaseService(string databaseUrl, string databasePath)
    {
        
        public async Task ReadDataAsync()
        {
            try
            {
                using HttpClient client = new();
                HttpResponseMessage response = await client.GetAsync($"{databaseUrl}/{databasePath}");
                response.EnsureSuccessStatusCode(); // Throw exception if not successful

                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Read data from Firebase Realtime Database:");
                Console.WriteLine(responseBody);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading data from Firebase Realtime Database: {ex.Message}");
            }
        }

        public async Task WriteDataAsync(string databaseWriteUrl,string dataPath, string newData)
        {
            try
            {
                using HttpClient client = new();
                StringContent content = new($"\"{newData}\"");
                HttpResponseMessage response = await client.PutAsync($"{databaseWriteUrl}/{dataPath}", content);
                response.EnsureSuccessStatusCode(); // Throw exception if not successful

                Console.WriteLine($"Data written to Firebase Realtime Database: {newData}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing data to Firebase Realtime Database: {ex.Message}");
            }
        }
    }
}
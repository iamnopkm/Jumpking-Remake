using System.Diagnostics;
using System.Net;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Debug = UnityEngine.Debug;
public class FileDataHandler 
{
    private string dataDirPath = "";
    
    private string dataFileName = "";
    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public GameData Load(string profileId)
    {
        string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);
        GameData loadedData = null;
        if(File.Exists(fullPath))
        {
            try
            {
                //loaded the serialized data from the file
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using(StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                //deserialize the data from the Json back into the C# object
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }catch(Exception e)
            {
                Debug.Log("Error occured when trying to load data from file " + fullPath + "\n" + e);
            }
        }
        return loadedData;
    }
    public void Save(GameData data, string profileId)
    {
        //use Path.Combine to account for different OS's having different path seperators
        string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);
        Debug.Log(fullPath);
        try
        {
            //create directory path if not exist
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            //Serialize the C# game data object into JSON
            string dataToStore = JsonUtility.ToJson(data, true);
            //Write the serialized data to the file
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (System.Exception ex)
        {
            Debug.Log("An exception occurred: " + ex.Message);
        }

    }
    public Dictionary<string, GameData> LoadAllProfiles()
    {   
        Dictionary<string, GameData> profileDictionary = new Dictionary<string, GameData>();
        // loop over all directory names in the data directory path
        IEnumerable<DirectoryInfo> dirInfos = new DirectoryInfo(dataDirPath).EnumerateDirectories();
            foreach( DirectoryInfo dirInfo in dirInfos)
            {
                string profileId = dirInfo.Name;
    
                // defensive programming - check if the data file exists
                // if it doesn't, then thiss foleder ins't a profile and should be skip
                string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);
                if(!File.Exists(fullPath))
                {
                    Debug.LogWarning("Skipping directory when loading all profiles because it does not contain data: " + profileId);
                    continue;
                }
    
                // Load the game data for this profile and put it in the dictionary 
                GameData profileData = Load(profileId);
                if(profileData != null)
                {
                    profileDictionary.Add(profileId, profileData);
                }
            }
        return profileDictionary;
    
    }
    
}

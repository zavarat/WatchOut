using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using System.Text;

[Serializable]
public class UserGameData
{
    private GameTime gameTime;
    public void SetGameTime(GameTime _timer) { gameTime = _timer; }
    public GameTime GetGameTime() { return gameTime; }
}

public class UserDataProcess : MonoBehaviour {
    
    private string fileDirectory;
    private UserGameData userGameData;
    public void InitData()
    {
        userGameData = new UserGameData();

        
        if(Application.platform == RuntimePlatform.WindowsEditor)
        {
            fileDirectory = Application.persistentDataPath + "/WatchOut_User.datas";
        }
        else if(Application.platform == RuntimePlatform.WindowsPlayer)
        {
            fileDirectory = Application.persistentDataPath + "/WatchOut_User.datas";
        }
        else if(Application.platform == RuntimePlatform.Android)
        {
            //fileDirectory = Application.persistentDataPath;
            //fileDirectory = fileDirectory.Substring(0, fileDirectory.LastIndexOf('/'));
            //fileDirectory = Path.Combine(fileDirectory, "Watch.data");
            
        }
    }

    public void SaveUserDataForWindows(UserGameData _userGameData)
    {
        userGameData = _userGameData;
        // 파일 생성.
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fileStream = File.Open(fileDirectory, FileMode.OpenOrCreate);
        // 시리얼라이징.
        bf.Serialize(fileStream, userGameData);
        fileStream.Close();
    }

    public void LoadUserDataForWindows()
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fileStream = File.Open(fileDirectory, FileMode.Open, FileAccess.Read, FileShare.Read);
            userGameData = (UserGameData)bf.Deserialize(fileStream);
            fileStream.Close();
        }
        catch (FileNotFoundException e)
        {
            
            GameTime gameTime = new GameTime();
            gameTime.InitTime();
            userGameData.SetGameTime(gameTime);
        }
        finally
        {
            // to do
        }
            
    }

    public UserGameData GetUserGameRecord()
    {
        return userGameData;
    }
}

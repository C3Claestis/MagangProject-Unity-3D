namespace Database
{
    using System.IO;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public static class SaveSystem
    {
        private static readonly string SAVE_FOLDER = Application.dataPath + "/Saves/";

        public static void Init()
        {
            if (!Directory.Exists(SAVE_FOLDER))
            {
                Directory.CreateDirectory(SAVE_FOLDER);
            }
        }

        public static void Save(string savestring)
        {
            File.WriteAllText(SAVE_FOLDER + "save.txt", savestring);
        }

        public static string Load()
        {
            if(File.Exists(SAVE_FOLDER + "save.txt")){
                 string savestring = File.ReadAllText(SAVE_FOLDER + "save.txt");
                 return savestring;
            }else{
                return null;
            }
        }
    }
}
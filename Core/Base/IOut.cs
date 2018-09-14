using System;

namespace Core.Base
{
    public interface IOut
    {
         bool LoadMasterDict();

         bool LoadSkin();

         bool LoadUserDict();

         bool LoadEnDict();

         bool LoadSettting();

         bool SaveSkin();

         bool SaveMasterDict();

         bool SaveUserDict();

         bool SaveEnDict();

         bool SaveSetting();

         bool CreateUI();

         bool InputIni();
    }
}

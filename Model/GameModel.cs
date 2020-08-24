using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JHchoi.Constants;

namespace JHchoi.Models
{
    public class GameModel : Model
    {
        //여기에는 테이블 같은 정적 데이터(모드별 Default 값)
        public ModelRef<SettingModel> setting = new ModelRef<SettingModel>();
        public ModelRef<PlayerModel> playerModel = new ModelRef<PlayerModel>();
        //public ModelRef<MonsterModel> monsterModel = new ModelRef<MonsterModel>();
        //public ModelRef<MapModel> mapModel = new ModelRef<MapModel>();

        public void Setup()
        {
            setting.Model = new SettingModel();
            setting.Model.Setup(this);

            playerModel.Model = new PlayerModel();
            playerModel.Model.Setup(this, "PlayerSetting");

            //monsterModel.Model = new MonsterModel();
            //monsterModel.Model.Setup(this, "MonsterSetting");

            //mapModel.Model = new MapModel();
            //mapModel.Model.Setup(this, "MapSetting");
        }
    }
}


using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public static class GameSignals
{
   public static Signal OnUpdateGold = new Signal();
   

   public static Signal SignalAreaCompleted = new Signal();
   public static Signal SignalQuestProgressChange = new Signal();
   public static Signal<bool> SignalCheckQuestAreaCompleted = new Signal<bool>();
   public static Signal  SignalBuildingShowAnimCompleted = new Signal();
   public static Signal SignalAllBuildingCompleted = new Signal();
   public static Signal<List<string>> SignalBuildingShow = new Signal<List<string>>();
}

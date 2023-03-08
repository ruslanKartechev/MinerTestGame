using System.Collections;
using Game.Main;
using UnityEngine;

namespace Saving
{
    public class PeriodicDataSaver : MonoBehaviour
    {
        [SerializeField] private DataSaver _saver;
        [SerializeField] private GlobalData _data;

        private void Start()
        {
            StartCoroutine(PeriodicSaving(_data.DataSavePeriod));
        }

        private IEnumerator PeriodicSaving(float period)
        {
            while (true)
            {
                yield return new WaitForSecondsRealtime(period);
                try
                {
                    Debug.Log("Periodic Saving!");
                    _saver.SaveData();
                }
                catch(System.Exception ex)
                {
                    Debug.Log($"Can't save!. {ex.Message}\n {ex.StackTrace}");
                }
            }
        }
    }
}
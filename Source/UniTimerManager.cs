using UnityEngine;
using JetBrains.Annotations;
using System.Collections.Generic;

namespace UnityTimer {
    /// <summary>
    /// Manages updating all the <see cref="UniTimer"/>s that are running in the application.
    /// This will be instantiated the first time you create a timer -- you do not need to add it into the
    /// scene manually.
    /// </summary>
    public class UniTimerManager : MonoBehaviour
    {
        private List<UniTimer> _timers = new List<UniTimer>();

        // buffer adding timers so we don't edit a collection during iteration
        private List<UniTimer> _timersToAdd = new List<UniTimer>();

        public void RegisterTimer(UniTimer timer)
        {
            this._timersToAdd.Add(timer);
        }

        internal void CancelAllTimers()
        {
            foreach (UniTimer timer in this._timers)
            {
                timer.Cancel();
            }

            this._timers = new List<UniTimer>();
            this._timersToAdd = new List<UniTimer>();
        }

        internal void PauseAllTimers()
        {
            foreach (UniTimer timer in this._timers)
            {
                timer.Pause();
            }
        }

        internal void ResumeAllTimers()
        {
            foreach (UniTimer timer in this._timers)
            {
                timer.Resume();
            }
        }

        // update all the registered timers on every frame
        [UsedImplicitly]
        private void Update()
        {
            this.UpdateAllTimers();
        }

        private void UpdateAllTimers()
        {
            if (this._timersToAdd.Count > 0)
            {
                this._timers.AddRange(this._timersToAdd);
                this._timersToAdd.Clear();
            }

            foreach (UniTimer timer in this._timers)
            {
                timer.Update();
            }

            this._timers.RemoveAll(t => t.isDone);
        }
    }
}

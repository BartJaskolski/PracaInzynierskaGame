using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace GameRunningCube.Source.GameEngine.Input
{
    public class KeyboardController
    {
        public KeyboardState newKeyboard, oldKeyboard;

        public List<KeyHandler> pressedKeys = new List<KeyHandler>();
        public List<KeyHandler> previousPressedKeys = new List<KeyHandler>();

        public KeyboardController()
        {
        }

        public virtual void Update()
        {
            newKeyboard = Keyboard.GetState();
            GetPressedKeys();
        }

        public void UpdateOld()
        {
            oldKeyboard = newKeyboard;

            previousPressedKeys = new List<KeyHandler>();
            foreach (var keyHandler in pressedKeys)
                previousPressedKeys.Add(keyHandler);
            
        }


        public bool GetPress(string KEY)
        {
            foreach (var keyHandler in pressedKeys)
            {
                if (keyHandler.key == KEY)
                    return true;
            }
            return false;
        }


        public virtual void GetPressedKeys()
        {
            pressedKeys.Clear();
            for (int i = 0; i < newKeyboard.GetPressedKeys().Length; i++)
                pressedKeys.Add(new KeyHandler(newKeyboard.GetPressedKeys()[i].ToString(), 1));
        }

    }
}

/*
Yarn Spinner is licensed to you under the terms found in the file LICENSE.md.
*/

using UnityEngine;
using System.Collections;
using System.Linq;

namespace Yarn.Unity.Example {

    [RequireComponent (typeof (SpriteRenderer))]
    /// Attach SpriteSwitcher to game object
    public class SpriteSwitcher : MonoBehaviour {

        [System.Serializable]
        public struct SpriteInfo {
            public string name;
            public Sprite sprite;
        }

        public SpriteInfo[] sprites;

        /// Create a command to use on a sprite
        [YarnCommand("setsprite")]
        public void UseSprite(string spriteName) {

            Sprite s = (from info in sprites where info.name == spriteName select info.sprite).FirstOrDefault();
            if (s == null) {
                Debug.LogErrorFormat("Can't find sprite named {0}!", spriteName);
                return;
            }

            GetComponent<SpriteRenderer>().sprite = s;
        }
    }

}

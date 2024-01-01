using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace FD.Core.Editors
{

    public class FAED_SettingEditor : EditorWindow
    {
        
        [MenuItem("FAED/Setting")]
        public static void CreateSettingWindow()
        {

            var window = GetWindow<FAED_SettingEditor>();
            window.titleContent.text = "FAED_Setting";
            window.maxSize = new Vector2(300, 500);
            window.minSize = new Vector2(300, 500);
            window.SettingWindow();
            window.Show();

        }

        

        private void SettingWindow()
        {
            var res = Resources.Load<FD.Core.FAED_SettingSO>("FAED/SettingSO");

            if (res == null)
            {
                if (!Directory.Exists(Application.dataPath + "/Resources"))
                {

                    Directory.CreateDirectory(Application.dataPath + "/Resources");

                }

                if (!Directory.Exists(Application.dataPath + "/Resources/FAED"))
                {

                    Directory.CreateDirectory(Application.dataPath + "/Resources/FAED");

                }

                var obj = CreateInstance<FAED_SettingSO>();
                AssetDatabase.CreateAsset(obj, "Assets/Resources/FAED/SettingSO.Asset");
                res = Resources.Load<FAED_SettingSO>("FAED/SettingSO");
                EditorUtility.SetDirty(res);

                AssetDatabase.Refresh();

            }

            var image = new Image();

            Texture2D texture = Resources.Load<Texture2D>("FAED_Logo");

            image.image = texture;
            image.style.flexShrink = 100;
            image.style.flexGrow = 0.3f;

            Label label = new Label("FAED_Setting");
            Toggle toggle = new Toggle("UsePooling");
            Button settingCompleteButton = new Button(() =>
            {

                var so = Resources.Load<FAED_SettingSO>("FAED/SettingSO");

                so.usePooling = toggle.value;

                if (so.usePooling && Resources.Load<FAED_PoolingSO>("FAED/PoolingSO") == null)
                {

                    var obj = CreateInstance<FAED_PoolingSO>();
                    AssetDatabase.CreateAsset(obj, "Assets/Resources/FAED/PoolingSO.Asset");
                    EditorUtility.SetDirty(obj);
                    res.poolingSO = Resources.Load<FAED_PoolingSO>("FAED/PoolingSO");

                }

                EditorUtility.SetDirty(res);
                AssetDatabase.SaveAssets();

                Close();

            });

            settingCompleteButton.text = "SettingComplete";

            toggle.value = res.usePooling;

            rootVisualElement.Add(image);
            rootVisualElement.Add(label);
            rootVisualElement.Add(toggle);
            rootVisualElement.Add(settingCompleteButton);

        }

    }

}
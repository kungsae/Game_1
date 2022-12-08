using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{
    [SerializeField] private Text nameText;
    [SerializeField] private Text typeText;
    [SerializeField] private Text infoText;

    public void SetInfo(WeaponData data)
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendFormat("Attack : {0}", data.attackP);
        builder.AppendLine();
        builder.AppendFormat("Speed : {0}", data.coolDown);
        builder.AppendLine();
        builder.AppendFormat("KnockBack : {0}", data.knockbackP);
        builder.AppendLine();

        nameText.text = data.name;
        infoText.text = builder.ToString();
        builder.Clear();
        builder.AppendFormat("Type : {0}", data.type);
        typeText.text = builder.ToString();
    }
}

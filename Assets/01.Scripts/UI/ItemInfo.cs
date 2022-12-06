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
        builder.Append("Attack : ");
        builder.Append(data.attackP);
        builder.AppendLine();
        builder.Append("Speed : ");
        builder.Append(data.coolDown);
        builder.AppendLine();
        builder.Append("KnockBack : ");
        builder.Append(data.knockbackP);
        builder.AppendLine();


        nameText.text = data.name;
        infoText.text = builder.ToString();
    }
}

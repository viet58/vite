using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class XuLiVaCham : MonoBehaviour
{
    public int Diem;
    public TextMeshProUGUI DiemSo;
    public TextMeshProUGUI Point;
    // Start is called before the first frame update
    void Start()
    {
        DiemSo.SetText(Diem.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Rac"))
        {
            Destroy(collision.gameObject);
            Diem = Diem + 100;
            DiemSo.SetText(Diem.ToString());
            Point.SetText(Diem.ToString() + " POINTS");

        }
    }
}

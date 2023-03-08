using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatManager : Singleton<CatManager>
{
    [SerializeField] GameObject BlackCat;

    [SerializeField] Button CatCreatButton;
    void Start()
    {
        CatCreatButton.onClick.AddListener(() => _CreatCat());
    }

    void Update()
    {
        
    }


    void _CreatCat()
    {
        ChierScript _SelectSeat = RandomSeat(ChierManager.Instance.ChierNum);
        Vector3 _SeatPos = _SelectSeat.transform.position;
        GameObject obj = Instantiate(BlackCat,_SeatPos,Quaternion.identity);

        _SelectSeat._ObjectSit(obj);
    }
    ChierScript RandomSeat(List<ChierScript> _seatlist)
    {
        int rand = Random.Range(0,_seatlist.Count);
        bool CanCreat = false;
        foreach(var list in _seatlist)
        {
            if(list.Sit_Obj == null)
                CanCreat = true;
        }
        if(CanCreat == true)
        {
            while(_seatlist[rand].Sit_Obj != null)
            {
                rand = Random.Range(0,_seatlist.Count);
            }
        return _seatlist[rand];
        }
        else 
            return null;
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class GalleryManager_Choose : MonoBehaviour
{
    public int sum_type;
    private List<bl_UGFInfo> Item_type = new List<bl_UGFInfo>();
    public int sum_furniture;
    private List<bl_UGFInfo> Item_furniture = new List<bl_UGFInfo>();

    [Header("Gallery")]
    public GameObject Gallery_type = null;
    public GameObject Gallery_furniture = null;

    [Header("References")]
    public GameObject ItemPrefab = null;
    public Transform Gallery_type_Panel = null;
    public Transform Gallery_furniture_Panel = null;
    [SerializeField] private ScrollRect ScrollList_type;
    [SerializeField] private ScrollRect ScrollList_furniture;

    public const string UGFName = "UGalleryManager";
    private List<bl_GalleryItem> type = new List<bl_GalleryItem>();
    private List<bl_GalleryItem> furniture = new List<bl_GalleryItem>();

    private int ID_compass;
    private int ID_room;
    public enum Mode {Compass, Room };
    public Mode mode_;

    public GameObject result;
    private GameObject DataManager;

    Evaluation.Direction temp_compass = new Evaluation.Direction();
    Evaluation.Room temp_room = new Evaluation.Room();

    private void Start()
    {
        DataManager = GameObject.Find("DataManager");

        gameObject.name = UGFName;

        mode_ = Mode.Compass;

        for (int i = 0; i < sum_type; i++)
        {
            Item_type.Add(new bl_UGFInfo());
            Item_type[i].set_item(Instantiate(ItemPrefab) as GameObject);
            Item_type[i].read_item().name = i.ToString();
            Item_type[i].read_item().transform.SetParent(Gallery_type_Panel, false);
            bl_GalleryItem gi = Item_type[i].read_item().GetComponent<bl_GalleryItem>();
            type.Add(gi);

            type[i].GetInfo(i, Resources.Load<Sprite>("main/main_" + (i + 1)), bl_GalleryItem.Mode.Type);
        }
    }

    public void Init_furniture()
    {
        for (int i = 0; i < sum_furniture; i++)
        {
            Item_furniture.Add(new bl_UGFInfo());
            Item_furniture[i].set_item(Instantiate(ItemPrefab) as GameObject);
            Item_furniture[i].read_item().name = i.ToString();
            Item_furniture[i].read_item().transform.SetParent(Gallery_furniture_Panel, false);
            bl_GalleryItem gi = Item_furniture[i].read_item().GetComponent<bl_GalleryItem>();
            furniture.Add(gi);

            furniture[i].GetInfo(i, Resources.Load<Sprite>("main" + i), bl_GalleryItem.Mode.Furniture);
        }
    }

    private void Update()
    {
        for (int i = 0; i < type.Count; i++)
        {
            if (Gallery_type.activeSelf == true) type[i].OnUpdate();
        }

        for (int i = 0; i < furniture.Count; i++)
        {
            if (Gallery_furniture.activeSelf == true) furniture[i].OnUpdate();
        }
    }

    public void GoToSideList_type(bool left)
    {
        StopAllCoroutines();
        StartCoroutine(IEMoveSide_type(left));
    }

    public void GoToSideList_furniture(bool left)
    {
        StopAllCoroutines();
        StartCoroutine(IEMoveSide_furniture(left));
    }

    public void Level(int id)
    {
        if (mode_ == Mode.Compass)
        {
            Gallery_type.SetActive(false);
            ScrollList_furniture.horizontalNormalizedPosition = 0;
            Gallery_furniture.SetActive(true);
            Init_furniture();

            ID_compass = id;
            mode_ = Mode.Room;
        }
        else if (mode_ == Mode.Room)
        {
            ID_room = id;
            NextGallery_furniture();
            result.SetActive(true);

            string compass_string = "";

            if (ID_compass == 0)
            {
                temp_compass = Evaluation.Direction.North;
                compass_string = "北";
            }
            else if (ID_compass == 1)
            {
                temp_compass = Evaluation.Direction.NorthEast;
                compass_string = "北東";
            }
            else if (ID_compass == 2)
            {
                temp_compass = Evaluation.Direction.East;
                compass_string = "東";
            }
            else if (ID_compass == 3)
            {
                temp_compass = Evaluation.Direction.SouthEast;
                compass_string = "南東";
            }
            else if (ID_compass == 4)
            {
                temp_compass = Evaluation.Direction.South;
                compass_string = "南";
            }
            else if (ID_compass == 5)
            {
                temp_compass = Evaluation.Direction.SouthWest;
                compass_string = "南西";
            }
            else if (ID_compass == 6)
            {
                temp_compass = Evaluation.Direction.West;
                compass_string = "西";
            }
            else if (ID_compass == 7)
            {
                temp_compass = Evaluation.Direction.NorthWest;
                compass_string = "北西";
            }

            string room_string = "";

            if (ID_room == 0)
            {
                temp_room = Evaluation.Room.Bedroom;
                room_string = "寝室";
            }
            else if (ID_room == 1)
            {
                temp_room = Evaluation.Room.Workroom;
                room_string = "仕事部屋";
            }
            else if (ID_room == 2)
            {
                temp_room = Evaluation.Room.Living;
                room_string = "リビング";
            }
            else if (ID_room == 3)
            {
                temp_room = Evaluation.Room.Kitchen;
                room_string = "台所";
            }
            else if (ID_room == 4)
            {
                temp_room = Evaluation.Room.Bathroom;
                room_string = "浴室";
            }

            result.GetComponent<Text>().text = "方角 " + compass_string + ", 部屋" + room_string;           
        }
    }

    public void CloseGallery_room()
    {
        GameObject.Find("DataManager").GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Button_Click"));

        for (int i = 0; i < sum_furniture; i++)
        {
            Destroy(Item_furniture[0].read_item());
            Item_furniture.RemoveAt(0);
            furniture.RemoveAt(0);
        }

        mode_ = Mode.Compass;
        Gallery_furniture.SetActive(false);
        Gallery_type.SetActive(true);
    }

    public void NextGallery_furniture()
    {
        Gallery_furniture.SetActive(false);
        Gallery_type.SetActive(false);
    }

    public void Result(bool ok)
    {
        GameObject.Find("DataManager").GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Button_Click"));

        if (ok == true)
        {
            DataManager.GetComponent<DataManager>().set_direction(temp_compass);
            DataManager.GetComponent<DataManager>().set_room(temp_room);

            StartCoroutine(Sample("Game"));            
        }
        else if (ok == false)
        {
            result.SetActive(false);
            Gallery_furniture.SetActive(true);
        }

    }

    // コルーチン  
    private IEnumerator Sample(string name)
    {
        yield return new WaitForSeconds(2.0f);

        SceneManager.LoadScene(name);
    }

    IEnumerator IEMoveSide_type(bool left)
    {
        if (left)
        {
            while (ScrollList_type.horizontalNormalizedPosition > 0)
            {
                ScrollList_type.horizontalNormalizedPosition -= Time.deltaTime;
                yield return null;
            }
        }
        else
        {
            while (ScrollList_type.horizontalNormalizedPosition < 1)
            {
                ScrollList_type.horizontalNormalizedPosition += Time.deltaTime;
                yield return null;
            }
        }
    }

    IEnumerator IEMoveSide_furniture(bool left)
    {
        if (left)
        {
            while (ScrollList_furniture.horizontalNormalizedPosition > 0)
            {
                ScrollList_furniture.horizontalNormalizedPosition -= Time.deltaTime;
                yield return null;
            }
        }
        else
        {
            while (ScrollList_furniture.horizontalNormalizedPosition < 1)
            {
                ScrollList_furniture.horizontalNormalizedPosition += Time.deltaTime;
                yield return null;
            }
        }
    }
}
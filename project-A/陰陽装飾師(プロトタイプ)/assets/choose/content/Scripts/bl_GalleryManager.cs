using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class bl_GalleryManager : MonoBehaviour
{
    public int sum_level;
    private List<bl_UGFInfo> Item_level = new List<bl_UGFInfo>();
    public int sum_type;
    private List<bl_UGFInfo> Item_type = new List<bl_UGFInfo>();
    public List<int> sum_furniture;
    private List<bl_UGFInfo> Item_furniture = new List<bl_UGFInfo>();

    [Header("Gallery")]
    public GameObject Gallery_level = null;
    public GameObject Gallery_type = null;
    public GameObject Gallery_furniture = null;

    [Header("FullWindow")]
    public GameObject FullWindow = null;
    public Image m_FullIcon = null;
    public Text m_TextName = null;
    public Text m_FullInfoText = null;
    public Button FullButtonOption = null;

    [Header("References")]
    public GameObject ItemPrefab = null;
    public Transform Gallery_level_Panel = null;
    public Transform Gallery_type_Panel = null;
    public Transform Gallery_furniture_Panel = null;
    [SerializeField] private ScrollRect ScrollList_level;
    [SerializeField] private ScrollRect ScrollList_type;
    [SerializeField] private ScrollRect ScrollList_furniture;

    public const string UGFName = "UGalleryManager";
    private List<bl_GalleryItem> level = new List<bl_GalleryItem>();
    private List<bl_GalleryItem> type = new List<bl_GalleryItem>();
    private List<bl_GalleryItem> furniture = new List<bl_GalleryItem>();

    public bool start_level;
    private int ID_level;
    //(運気の)ノルマ変数
    //[0] = 仕事運，[1] = 人気運, [2] = 健康運, [3] = 金運, [4] = 恋愛運
    private int[] norma_luck_ = new int[5];
    //アドバイスモード(0 = 仕事運重視，1 = 人気運重視，2 = 健康運重視，3 = 金運重視，4 = 恋愛運重視, 5 = デフォルト(ノルマ重視))
    private int advaice_mode_;
    private Evaluation.Direction temp_compass = new Evaluation.Direction();
    private Evaluation.Room temp_room = new Evaluation.Room();
    public List<GameObject> room = new List<GameObject>();
    public GameObject result;
    private GameObject DataManager;

    private FurnitureGrid FurnitureGrid;
    private int furniture_ID_; //仮家具グリッドID
    public enum Mode {Add, Change, Property };
    public Mode mode_;

    public FurnitureManagement FurnitureManagement;
    public GameObject Bottom;

    private bool once_true = true;
    private bool once_false = true;

    private void Start()
    {
        gameObject.name = UGFName;

        DataManager = GameObject.Find("DataManager");

        if (start_level == true)
        {
            Init_level();
        }

    }

    public void Init_level()
    {
        for (int i = 0; i < sum_level; i++)
        {
            Item_level.Add(new bl_UGFInfo());
            Item_level[i].set_item(Instantiate(ItemPrefab) as GameObject);
            Item_level[i].read_item().name = i.ToString();
            Item_level[i].read_item().transform.SetParent(Gallery_level_Panel, false);
            bl_GalleryItem gi = Item_level[i].read_item().GetComponent<bl_GalleryItem>();
            level.Add(gi);

            if (i == 0)
            {
                level[i].GetInfo(i, Resources.Load<Sprite>("youkai/work/body"), bl_GalleryItem.Mode.Level);
            }
            if (i == 1)
            {
                level[i].GetInfo(i, Resources.Load<Sprite>("youkai/popular/body"), bl_GalleryItem.Mode.Level);
            }
            if (i == 2)
            {
                level[i].GetInfo(i, Resources.Load<Sprite>("youkai/health/body"), bl_GalleryItem.Mode.Level);
            }
            if (i == 3)
            {
                level[i].GetInfo(i, Resources.Load<Sprite>("youkai/economic/body"), bl_GalleryItem.Mode.Level);
            }
            if (i == 4)
            {
                level[i].GetInfo(i, Resources.Load<Sprite>("youkai/love/body"), bl_GalleryItem.Mode.Level);
            }

        }
    }

    public void Init_type()
    {
        for (int i = 0; i < sum_type; i++)
        {
            Item_type.Add(new bl_UGFInfo());
            Item_type[i].set_item(Instantiate(ItemPrefab) as GameObject);
            Item_type[i].read_item().name = i.ToString();
            Item_type[i].read_item().transform.SetParent(Gallery_type_Panel, false);
            bl_GalleryItem gi = Item_type[i].read_item().GetComponent<bl_GalleryItem>();
            type.Add(gi);

            type[i].GetInfo(i, Resources.Load<Sprite>("main/main_" + i), bl_GalleryItem.Mode.Type);
        }
    }

    public void Init_furniture(FurnitureGrid furnituregrid)
    {
        FurnitureGrid = furnituregrid;

        for (int i = 0; i < sum_furniture[FurnitureGrid.category_ID()]; i++)
        {
            Item_furniture.Add(new bl_UGFInfo());
            Item_furniture[i].set_item(Instantiate(ItemPrefab) as GameObject);
            Item_furniture[i].read_item().name = i.ToString();
            Item_furniture[i].read_item().transform.SetParent(Gallery_furniture_Panel, false);
            bl_GalleryItem gi = Item_furniture[i].read_item().GetComponent<bl_GalleryItem>();
            furniture.Add(gi);

            furniture[i].GetInfo(i, Resources.Load<Sprite>(furnituregrid.furniture_type().ToString() + "/" + furnituregrid.furniture_type().ToString() + "_" + (i + 1)), bl_GalleryItem.Mode.Furniture);
        }
    }

    private void Update()
    {
        for (int i = 0; i < level.Count; i++)
        {
            if (Gallery_level.activeSelf == true) level[i].OnUpdate();
        }

        if (start_level == true)
        {            
            if (gameObject.GetComponent<AudioSource>().isPlaying == true && once_true == true)
            {
                GameObject.Find("DataManager").GetComponent<AudioSource>().volume = 0.3f;
                once_true = false;
                once_false = true;
            }
            else if(gameObject.GetComponent<AudioSource>().isPlaying == false && once_false == true)
            {
                GameObject.Find("DataManager").GetComponent<AudioSource>().volume = 1.0f;
                once_false = false;
                once_true = true;                
            }
        }

        for (int i = 0; i < type.Count; i++)
        {
            if (Gallery_type.activeSelf == true) type[i].OnUpdate();
        }

        for (int i = 0; i < furniture.Count; i++)
        {
            if (Gallery_furniture.activeSelf == true) furniture[i].OnUpdate();
        }
    }

    public void GoToSideList_level(bool left)
    {
        StopAllCoroutines();
        StartCoroutine(IEMoveSide_level(left));
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

    /// <summary>
    /// クリックしたアイテムから情報を取得し、フルウィンドウに送信して開く
    /// </summary>
    //変更から

    public void Level(int id)
    {
        ID_level = id;

        if (ID_level == 0)
        {
            gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("youkai/work/voice");
        }
        else if (ID_level == 1)
        {
            gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("youkai/popular/voice");
        }
        else if (ID_level == 2)
        {
            gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("youkai/health/voice");
        }
        else if (ID_level == 3)
        {
            gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("youkai/economic/voice");
        }
        else if (ID_level == 4)
        {
            gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("youkai/love/voice");
        }

        gameObject.GetComponent<AudioSource>().Play();

        StartCoroutine(Wait(ID_level));
    }

    // コルーチン  
    private IEnumerator Wait(int id)
    {
        yield return new WaitForSeconds(1.0f);

        NextGallery_level();
        //ScrollList_level.horizontalNormalizedPosition = 0;
        result.SetActive(true);

        room[ID_level].SetActive(true);

        for (int i = 0; i < norma_luck_.Length; i++)
        {
            if (i == ID_level)
            {
                norma_luck_[i] = 200;
            }
            else
            {
                norma_luck_[i] = 50;
            }
        }

        advaice_mode_ = ID_level;

        string compass_string = "";

        int ID_compass = Random.Range(0, 8);

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

        int ID_room = Random.Range(0, 5);
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

        //result.GetComponent<Text>().text = "方角 " + compass_string + ", 部屋" + room_string;
        result.transform.Find("compass&room").gameObject.GetComponent<Text>().text = "「方角」" + compass_string + ", 「部屋」" + room_string;
        //result.transform.Find("compass&room").gameObject.transform.position = room[ID_level].transform.position + new Vector3(0,10,0);
    }

    public void FullWindow_furniture(int id, Sprite sprite)
    {
        furniture_ID_ = id;
        m_TextName.text = FurnitureGrid.furniture_type().ToString() + furniture_ID_;
        m_FullIcon.sprite = sprite;        
    }

    //属性から
    public void FullWindow_property(FurnitureGrid furnituregrid)
    {
        m_TextName.text = furnituregrid.object_name();
        m_FullIcon.sprite = furnituregrid.read_sprite();
    }

    public void OpenGallery_level()
    {
        ScrollList_level.horizontalNormalizedPosition = 0;
        Gallery_level.SetActive(true);
    }

    public void NextGallery_level()
    {
        GameObject.Find("DataManager").GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Button_Click"));

        Gallery_level.SetActive(false);
    }

    public void CloseResult()
    {
        GameObject.Find("DataManager").GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Button_Click"));
        room[ID_level].SetActive(false);
        result.SetActive(false);
        OpenGallery_level();
    }

    public void OpenGallery_type()
    {
        ScrollList_type.horizontalNormalizedPosition = 0;
        Gallery_type.SetActive(true);
    }

    public void CloseGallery_type()
    {
        GameObject.Find("DataManager").GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Button_Click"));

        Destroy_type();

        Gallery_type.SetActive(false);
        FurnitureManagement.move_furniture = true;
        FurnitureManagement.Add_ChangeMode(true);
    }

    public void Destroy_type()
    {
        for (int i = 0; i < sum_type; i++)
        {
            Destroy(Item_type[0].read_item());
            Item_type.RemoveAt(0);
            type.RemoveAt(0);
        }
    }

    public void NextGallery_type()
    {
        Gallery_type.SetActive(false);
    }

    public void OpenGallery_furniture()
    {
        ScrollList_furniture.horizontalNormalizedPosition = 0;
        Gallery_furniture.SetActive(true);
    }

    public void CloseGallery_furniture()
    {
        GameObject.Find("DataManager").GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Button_Click"));

        Destroy_furniture();

        Gallery_furniture.SetActive(false);

        if (mode_ == Mode.Add)
        {
            Gallery_type.SetActive(true);
        }
        else
        {
            FurnitureManagement.Add_ChangeMode(true);
        }
    }

    public void Destroy_furniture()
    {
        for (int i = 0; i < sum_furniture[FurnitureGrid.category_ID()]; i++)
        {
            Destroy(Item_furniture[0].read_item());
            Item_furniture.RemoveAt(0);
            furniture.RemoveAt(0);
        }
    }

    public void NextGallery_furniture()
    {
        Gallery_furniture.SetActive(false);
    }

    public void OpenFullWindow()
    {
        FullWindow.SetActive(true);

        if (mode_ == Mode.Property)
        {
            FullButtonOption.gameObject.SetActive(false);
        }
        else
        {
            FullButtonOption.gameObject.SetActive(true);
        }
    }

    public void CloseFullWindow()
    {
        GameObject.Find("DataManager").GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Button_Click"));

        FullWindow.SetActive(false);

        if (mode_ != Mode.Property)
        {
            Gallery_furniture.SetActive(true);
        }
              
    }

    public void Result(bool ok)
    {
        GameObject.Find("DataManager").GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Button_Click"));

        if (ok == true)
        {
            DataManager.GetComponent<DataManager>().set_direction(temp_compass);
            DataManager.GetComponent<DataManager>().set_room(temp_room);
            DataManager.GetComponent<DataManager>().set_norma_luck(norma_luck_);
            DataManager.GetComponent<DataManager>().set_advaice_mode(advaice_mode_);
            GameObject.Find("DataManager").GetComponent<AudioSource>().volume = 1.0f;
            StartCoroutine(Sample("Game"));
        }
        else if (ok == false)
        {
            result.SetActive(false);
            Gallery_level.SetActive(true);
        }

    }

    // コルーチン  
    private IEnumerator Sample(string name)
    {
        yield return new WaitForSeconds(2.0f);

        SceneManager.LoadScene(name);
    }

    public void OK()
    {
        GameObject.Find("DataManager").GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Button_Click"));

        if (mode_ == Mode.Add)
        {
            FullWindow.SetActive(false);
            Destroy_furniture();
            Gallery_furniture.SetActive(false);
            Destroy_type();
            Gallery_type.SetActive(false);

            FurnitureManagement.AddFurniture(FurnitureGrid.category_ID(), furniture_ID_);
        }
        else if (mode_ == Mode.Change)
        {
            FullWindow.SetActive(false);
            Destroy_furniture();
            Gallery_furniture.SetActive(false);

            FurnitureManagement.Menu.SetActive(false);
            FurnitureManagement.ChangeFurniture(FurnitureGrid.category_ID(), furniture_ID_);
        }

        FurnitureManagement.Add_ChangeMode(true);
        FurnitureManagement.move_furniture = true;
    }

    IEnumerator IEMoveSide_level(bool left)
    {
        if (left)
        {
            while (ScrollList_level.horizontalNormalizedPosition > 0)
            {
                ScrollList_level.horizontalNormalizedPosition -= Time.deltaTime;
                yield return null;
            }
        }
        else
        {
            while (ScrollList_level.horizontalNormalizedPosition < 1)
            {
                ScrollList_level.horizontalNormalizedPosition += Time.deltaTime;
                yield return null;
            }
        }
    }

    IEnumerator IEMoveSide_type(bool left)
    {
        if (left)
        {
            while(ScrollList_type.horizontalNormalizedPosition > 0)
            {
                ScrollList_type.horizontalNormalizedPosition -= Time.deltaTime;
                yield return null;
            }
        }else
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
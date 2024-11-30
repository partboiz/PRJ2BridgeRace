using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public List<GameObject> listBrick = new List<GameObject>();
    [SerializeField] private Transform brickPoint;
    [SerializeField] private GameObject brickPre;
    Material randomMaterial;
    [SerializeField] public Renderer colorCharacter;
    public float heightIncrement = 1.0f;
    public Animator anim;
    private string currentAnim;


    private void Start()
    {
        OnInit();
    }
    public virtual void OnInit()
    {
        ChangeAnim(Constants.ANIM_IDLE);
        randomMaterial = DataManager.Instance.colorData.GetColorCharacter();
        colorCharacter.GetComponent<Renderer>().material = randomMaterial;
    }
    
    public virtual void AddBrick(GameObject brick)
    {
        Vector3 spawnPosition = brickPoint.position - new Vector3(0, -listBrick.Count * heightIncrement, 0);
        GameObject newBrickInstance = Instantiate(brickPre, spawnPosition, Quaternion.identity);
        Renderer brickRenderer = newBrickInstance.GetComponent<Renderer>();
        brickRenderer.material = colorCharacter.material;
        newBrickInstance.transform.SetParent(brickPoint, true);
        newBrickInstance.transform.localRotation = Quaternion.Euler(0, 0, 0);
        listBrick.Add(newBrickInstance);
    }
    protected virtual void RemoveBricks(int count)
    {
        count = Mathf.Min(count, listBrick.Count);
        for (int i = 0; i < count; i++)
        {
            GameObject brick = listBrick[listBrick.Count - 1];
            listBrick.RemoveAt(listBrick.Count - 1);
            Destroy(brick);
        }
        UpdateBrickPosition();
    }
    private void UpdateBrickPosition()
    {
        for (int i = 0; i < listBrick.Count; i++)
        {
            Vector3 newPosition = brickPoint.position - new Vector3(0, -i * heightIncrement, 0);
            listBrick[i].transform.position = newPosition;
        }
    }
    protected bool CheckColor(Material player, Material brick)
    {
        return player.color == brick.color;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Brick"))
        {
            Renderer brickColor = other.gameObject.GetComponent<Renderer>();
            if (!CheckColor(colorCharacter.material, brickColor.material)) return;
            AddBrick(other.gameObject);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Stair"))
        {
            ChangeStairColor(other.gameObject);
        }
    }
    private void ChangeStairColor(GameObject stair)
    {
        Renderer stairRenderer = stair.GetComponent<Renderer>();
        BoxCollider stairCollider = stair.GetComponent<BoxCollider>();
        if (stairRenderer != null && listBrick.Count > 0)
        {

            if (stairRenderer.material.color != colorCharacter.material.color)
            {
                stairRenderer.material.color = colorCharacter.material.color;
                RemoveBricks(1);


                if (stairCollider != null)
                {
                    Vector3 newSize = stairCollider.size;
                    newSize.y = Mathf.Max(newSize.y - heightIncrement, 0.1f);
                    stairCollider.size = newSize;

                    // Đặt lại vị trí trung tâm của Box Collider nếu cần
                    stairCollider.center = new Vector3(stairCollider.center.x, newSize.y / 2, stairCollider.center.z);
                }
            }

            ColorAdjacentStairs(stair);
        }
    }
    private void ColorAdjacentStairs(GameObject startStair)
    {
        GameObject currentStair = startStair;
        while (listBrick.Count > 0)
        {
            // Tìm bậc thang kế tiếp
            currentStair = GetNextStair(currentStair);
            if (currentStair == null) break;

            Renderer stairRenderer = currentStair.GetComponent<Renderer>();
            if (stairRenderer != null && stairRenderer.material.color != colorCharacter.material.color)
            {
                stairRenderer.material.color = colorCharacter.material.color;
                RemoveBricks(1);
            }
            else
            {
                // Nếu gặp bậc thang đã có màu, dừng quá trình
                break;
            }
        }
    }
    private GameObject GetNextStair(GameObject currentStair)
    {
        return null;
    }
    public void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            if (!string.IsNullOrEmpty(currentAnim))
            {
                anim.ResetTrigger(currentAnim);
            }

            currentAnim = animName;
            anim.SetTrigger(currentAnim);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HelpManager : MonoBehaviour
{
    public GameObject[] pageObjs;

    public Button prevBtn;
    public Button nextBtn;

    int currentPageNum = 0;

    public void PageBtn(int num)
    {
        pageObjs[currentPageNum].SetActive(false);

        switch(num)
        {
            case 0:
                if (currentPageNum == pageObjs.Length - 1)
                    nextBtn.interactable = true;

                if (currentPageNum != 0)
                    currentPageNum--;
                break;
            case 1:
                if(currentPageNum == 0)
                    prevBtn.interactable = true;

                if (currentPageNum != pageObjs.Length - 1)
                    currentPageNum++;
                break;
        }

        if(currentPageNum == 0)
        {
            prevBtn.interactable = false;
        }
        else if(currentPageNum == pageObjs.Length - 1)
        {
            nextBtn.interactable = false;
        }

        pageObjs[currentPageNum].SetActive(true);
    }

    public void ReturnTitle()
    {
        SceneManager.LoadScene(0);
    }

}

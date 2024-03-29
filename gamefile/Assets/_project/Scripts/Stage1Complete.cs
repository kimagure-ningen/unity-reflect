using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class Stage1Complete : MonoBehaviour
{
    [SerializeField] private GameObject chestCover;
    [SerializeField] private CinemachineVirtualCamera completeCam;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject player;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject joystick;
    [SerializeField] private GameObject gameClearUI;
    [SerializeField] private GameObject gameClearText1;
    [SerializeField] private GameObject gameClearText2;
    [SerializeField] private GameObject returnButton;
    [SerializeField] private BGMSwitcher bgmSwitcher;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            completeCam.Priority = 15;
            chestCover.transform.DOLocalRotate(new Vector3(70f, 0f, 0f), 1.5f).OnComplete(() =>
            {
                joystick.SetActive(false);
                playerController.isGameEnd = true;
                player.transform.position = new Vector3(184f, 3f, 0f);
                player.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
                anim.SetTrigger("Victory");
                Debug.Log("Stage 1 Complete!");
                bgmSwitcher.PlayBGM3();
                gameClearUI.SetActive(true);
                gameClearUI.transform.DOScale(new Vector3(1f, 1f, 1f), 0.75f).OnComplete(() =>
                {
                    gameClearText1.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.75f).SetLoops(-1, LoopType.Yoyo);
                    gameClearText2.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.75f).SetLoops(-1, LoopType.Yoyo);
                    returnButton.SetActive(true);
                });
            });
        }
    }
    
    public void ReturnToTitle()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
}

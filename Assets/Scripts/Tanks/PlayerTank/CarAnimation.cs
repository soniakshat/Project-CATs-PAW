using UnityEngine;

public class CarAnimation : MonoBehaviour
{
    [SerializeField] private Animator _carAnim;
    [SerializeField] private PlayerTankControl _ptc;


    void Start()
    {
        _carAnim = GetComponent<Animator>();
        _ptc = GetComponentInParent<PlayerTankControl>();
    }


    void Update()
    {
        if (_ptc._dirZ > 0)
        {
            _carAnim.SetBool("Fwd", true);
        }
        else if (_ptc._dirZ < 0)
        {
            _carAnim.SetBool("Bwd", true);
        }
        else if (_ptc._dirY > 0)
        {
            _carAnim.SetBool("Ri8", true);
        }
        else if (_ptc._dirY < 0)
        {
            _carAnim.SetBool("Lft", true);
        }
        else
        {
            _carAnim.SetBool("Fwd", false);
            _carAnim.SetBool("Bwd", false);
            _carAnim.SetBool("Ri8", false);
            _carAnim.SetBool("Lft", false);
        }
    }
}

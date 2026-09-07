using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
   [SerializeField] private int HP= 100;

    
    public float ms= 2f;
    protected Transform player;
    [Header ("Pengaturan State Machine")]
    [SerializeField] private float jarakDeteksi = 6f;
    [SerializeField] private float jarakSerang = 1.2f;
    [SerializeField] private float jedaSerang = 1f;

    private StateZombie state = StateZombie.IDLE;
    private float waktuSerangTerakhir;


    protected virtual void Start()
    //bisa di edit di child class, tapi kalau tidak di edit akan tetap jalan
    {
        GameObject playerobj = GameObject.FindGameObjectWithTag("Player");
        if (playerobj != null)
        {
            player = playerobj.transform;
        }
    }
    void Update()
    {
       
        PeriksaTransisi();

    //switch(state)
      //{
        //case StateZombie.IDLE: perilakuIdle(); break;
        //case StateZombie.PATROL: perilakuPatrol(); break;
        //case StateZombie.CHASE: perilakuChase(); break;
        //case StateZombie.ATTACK: perilakuAttack(); break;
      //}
    }
    public void Kejar()

    {

        if (player == null) return;


        transform.position = Vector2.MoveTowards(
            transform.position,
            player.position,
            ms * Time.deltaTime

        );

     }
     public virtual void Serang()
     {
        Debug.Log("Enemy Serang");
     }

    public float jarakKePlayer()
    {   
        if (player == null) return Mathf.Infinity;
        return Vector2.Distance(transform.position, player.position);
    }
    void PeriksaTransisi()
    {
        float jarak = jarakKePlayer();

        if (jarak <= jarakSerang)
            state = StateZombie.ATTACK;
        else if (jarak <= jarakDeteksi)
            state = StateZombie.CHASE;
        else
            state = StateZombie.PATROL;
    }

     void perilakuIdle()
     {
        Debug.Log(name + "IDLE");
     }
     void perilakuPatrol()
     {
        Debug.Log(name + "PATROL");
     }
     void perilakuChase()
     {
        Kejar();
        Debug.Log(name + "CHASE");
     }
     void perilakuAttack()
     {
        if (Time.time >= waktuSerangTerakhir + jedaSerang)
        {
            Serang();
            waktuSerangTerakhir = Time.time;
        }
     }
     public void KenaDamage(int jumlah)
     {
        HP -= jumlah;
        Debug.Log($"{gameObject.name} Kena Damage {jumlah}, HP: {HP}");
        if (HP <= 0)
        {
            Mati();
        }
     }
     protected virtual void Mati()
    {
    Debug.Log(gameObject.name + " Mati");
    Destroy(gameObject);
    }

}
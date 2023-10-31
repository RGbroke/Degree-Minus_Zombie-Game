using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    //Health
    public HealthBar healthBar;
    [SerializeField] public int maxHealth = 10;
    [HideInInspector] public int currHealth;
    

    //Movement
    [SerializeField] private float moveSpeed = 5f;

    private Vector2 moveDirection;
    private Vector2 aimDirection;

    [SerializeField] private Rigidbody2D controller;
    private PlayerControls playerControls;
    private PlayerInput playerInput;
    private bool isGamepad;

    //Weapon & Aim
    [SerializeField] private Pivot pivot;
    public Animator animator;
    [SerializeField] private Weapon weapon;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float fireRate;
    [SerializeField] private float grenadeCooldown;
    private float currGrenadeCooldown = 0;
    private bool fire;
    

    //Weapon SFX
    public AudioSource gunshotEnd;

    //Player Damage
    [SerializeField] private float damageRate = 1f;
    private bool hit = false;
    private float lastDamageTime;
    private int damageTaken;

    public SpriteRenderer sprite;
    public SpriteRenderer weaponSprite;

    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private Texture2D cursorTextureShoot;
    private Vector2 cursorHotspot;

    private void Start()
    {
        cursorHotspot = new Vector2(cursorTexture.width / 2, cursorTexture.height / 2);
        Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);
    }

    private void Awake()
    {
        Time.timeScale = 1f;
        playerInput = GetComponent<PlayerInput>();
        playerControls = new PlayerControls();
    }
    private void OnEnable()
    {
        //Health
        currHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        playerControls.Enable();
        playerControls.Controls.Fire.started += ctx => StartFiring();
        playerControls.Controls.Fire.canceled += ctx => StopFiring();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }
    public IEnumerator FlashRed()
    {
        sprite.color = Color.red;
        weaponSprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
        weaponSprite.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        currGrenadeCooldown -= Time.deltaTime;
        //Movement/Control handling
	    if(Time.timeScale != 0){
            HandleInput();
            HandleMovement();
            HandleAiming();
	    }

        if (fire)
        {
            Fire();
        }

        if(Time.time > lastDamageTime + damageRate && hit)
        {
            StartCoroutine(FlashRed());
            lastDamageTime = Time.time;
            if (currHealth - damageTaken > 0)
            {
                currHealth -= damageTaken;
            }
            else
            {
                currHealth = 0;
            }
            healthBar.SetHealth(currHealth);
        }
        hit = false;
    }

    void HandleInput() 
    {
        moveDirection = playerControls.Controls.Movement.ReadValue<Vector2>();
        aimDirection = playerControls.Controls.Aim.ReadValue<Vector2>();
    }
    void HandleMovement()
    {
        controller.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        animator.SetFloat("Horizontal", moveDirection.x);
        animator.SetFloat("Speed", moveDirection.sqrMagnitude);
    }
    void HandleAiming()
    {
        if(isGamepad) 
        {
            pivot.HandleAiming(aimDirection);
        } 
        else 
        { 
            pivot.HandleAiming(); 
        }

    }

    public void OnDeviceChange(PlayerInput i)
    {
        isGamepad = i.currentControlScheme.Equals("Gamepad") ? true : false;
    }

    private void StartFiring()
    {
        fire = true;
        Cursor.SetCursor(cursorTextureShoot, cursorHotspot, CursorMode.Auto);
    }
    private void StopFiring()
    {
        fire = false;
        gunshotEnd.Play();
        Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);
    }
    private void Fire()
    {
        weapon.Fire(bulletSpeed, fireRate);
    }
    public void Grenade()
    {
        if (currGrenadeCooldown <= 0)
        {
            weapon.ThrowGrenade(bulletSpeed / 4);
            currGrenadeCooldown = grenadeCooldown;
        }
        
    }
    public void Melee()
    {
       weapon.Melee();
    }

    public void TakeDamage(int damage)
    {
        damageTaken = damage;
        hit = true;
    }

    public void addHealth(int health)
    {
        if ((currHealth + health) <= maxHealth)
            currHealth += health;
        else
            currHealth = maxHealth;
        healthBar.SetHealth(currHealth);
    }

    public float getHealth()
    {
        return currHealth;
    }
}

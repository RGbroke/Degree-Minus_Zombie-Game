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

    //Controls
    [SerializeField] private Rigidbody2D controller;
    private PlayerControls playerControls;
    private PlayerInput playerInput;
    private bool isGamepad;

    //Weapon & Aim
    [SerializeField] private Pivot pivot;
    [SerializeField] private Weapon weapon;
    private bool fire;
    private bool automatic;

    //Grenade
    [SerializeField] private float grenadeSpeed = 5f;
    [SerializeField] private float grenadeCooldown;
    private float currGrenadeCooldown = 0;

    //Player Damage
    [SerializeField] private float damageRate = 1f;
    private bool hit = false;
    private float lastDamageTime;
    private int damageTaken;

    //Sprite & Animation
    public SpriteRenderer sprite;
    public SpriteRenderer weaponSprite;
    public Animator animator;

    //Cursor
    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private Texture2D cursorTextureShoot;
    private Vector2 cursorHotspot;

    //Melee
    public float meleeDelay;
    public float meleeDamage;
    public float knockOutTime;
    public float meleeForce;
    public float meleeRadius;

    private void Start()
    {
        cursorHotspot = new Vector2(cursorTexture.width / 2, cursorTexture.height / 2);
        Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);
        weapon.meleeDelay = meleeDelay;
        weapon.meleeDamage = meleeDamage;
        weapon.knockOutTime = knockOutTime;
        weapon.meleeForce = meleeForce;
        weapon.meleeRadius = meleeRadius;
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
        if(Time.timeScale != 0)
        {
            playerControls.Enable();
            playerControls.Controls.Fire.started += ctx => StartFiring();
            playerControls.Controls.Fire.canceled += ctx => StopFiring();
        }
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
    private void FixedUpdate()
    {
        if (Time.timeScale != 0)
        {
            HandleInput();
            HandleMovement();
            HandleAiming();
        }
    }
    void Update()
    {
        
        currGrenadeCooldown -= Time.deltaTime;
        //Movement/Control handling

        if (fire && automatic)
        {
            weapon.Fire();
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
        automatic = weapon.automatic;
        if (!automatic)
        {
            weapon.Fire();
        }
        Cursor.SetCursor(cursorTextureShoot, cursorHotspot, CursorMode.Auto);
    }

    private void StopFiring()
    {
        fire = false;
        Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);
    }
    
    public void Reload(InputAction.CallbackContext context)
    {
        if (!context.started || weapon.isReloading)
            return;

        weapon.reload();
    }
    public void cancelReload(InputAction.CallbackContext context)
    {
        if (!context.started || !weapon.isReloading)
            return;

        weapon.cancelReload();
    }
  
    public void Grenade()
    {
        if (currGrenadeCooldown <= 0)
        {
            weapon.ThrowGrenade(grenadeSpeed);
            currGrenadeCooldown = grenadeCooldown;
        }
        
    }

    public void Melee()
    {
        if(Time.timeScale != 0)
            {   
                weapon.Melee();
            }
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

    public void KnockBack(Vector2 direction)
    {
        Debug.Log(direction);
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(direction * 10000f, ForceMode2D.Force);
    }
}

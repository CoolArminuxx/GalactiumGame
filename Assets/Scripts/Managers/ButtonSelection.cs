using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSelection : MonoBehaviour
{
    [SerializeField] private TerminalManager TerminalRef;
    private Button ButtonRef;
    public bool IsRepairButton;

    private void OnEnable()
    {
        TerminalRef.CanSelect = false;
        ButtonRef = GetComponent<Button>();
        if (IsRepairButton == true)
        {
            ButtonRef.Select();
        }
    }
    public void Repair()
    {
        if (TerminalRef.CanSelect == true)
        {
            TerminalRef.RepairShip();
            TerminalRef.CanSelect = false;
        }
    }
    public void Slot1()
    {
        if (TerminalRef.CanSelect == true)
        {
            TerminalRef.ButtonSelected = 0;
            TerminalRef.UpgradeSelection();
            EventSystem.current.SetSelectedGameObject(null);
            TerminalRef.CanSelect = false;
        }
    }
    public void Slot2()
    {
        if (TerminalRef.CanSelect == true)
        {
            TerminalRef.ButtonSelected = 1;
            TerminalRef.UpgradeSelection();
            EventSystem.current.SetSelectedGameObject(null);
            TerminalRef.CanSelect = false;
        }
    }
    public void Slot3()
    {
        if (TerminalRef.CanSelect == true)
        {
            TerminalRef.ButtonSelected = 2;
            TerminalRef.UpgradeSelection();
            EventSystem.current.SetSelectedGameObject(null);
            TerminalRef.CanSelect = false;
        }
    }
}

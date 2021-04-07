using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipCalculator : MonoBehaviour
{
    [SerializeField] private float _subTotal;
    [SerializeField] private float _tipPct;
    [SerializeField] private float _tax;
    private float _taxAmt;
    private float _tipAmt;
    private float _totalAmt;
    void Start()
    {
        CalculateTotal();

        Debug.Log("Subtotal: $" + _subTotal);
        Debug.Log("Tip %: " + _tipPct);
        Debug.Log("Tax total: $" + _taxAmt);
        Debug.Log("Tip total: $" + _tipAmt);
        Debug.Log("Total with tax: $" + _totalAmt);
    }

    private void CalculateTotal()
    {
        _taxAmt = _subTotal * (_tax / 100);
        _tipAmt = _subTotal * (_tipPct / 100);
        _totalAmt = _subTotal +  _tipAmt + _taxAmt;
    }
}

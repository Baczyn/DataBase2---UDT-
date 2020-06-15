using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

[Serializable]
[Microsoft.SqlServer.Server.SqlUserDefinedType(Format.UserDefined, MaxByteSize = 8000, IsByteOrdered = true)]
public struct AccountNr : INullable, IBinarySerialize
{
    //variable for account number
    private string _accountNr;
    private bool m_Null;
    //variable for bank type
    private string _bankType;

    //constructor
    public AccountNr(string accountNr){
        _accountNr = accountNr;
        _bankType = "";
        m_Null = false;

    }

    //constructor
    public AccountNr(bool nothing)
    {
        _accountNr = _bankType = "";
        m_Null = true;
    }

    //enum holds id of bank types(2-6 numbers in account number)
    enum Bank{
        ING = 1050,
        NBP= 1010,  
    	PKO = 1020,
    	CitiHandlowy = 1030,
    	Santander = 1090,
    	mBank = 1140,
    	Millennium =1160
    }

    public override string ToString()
    {
        // Replace the following code with your code
        return _bankType+": "+ _accountNr;
    }

    //Method returns _accoutNr
    public string GetAccountNumber()
    {
        return _accountNr.ToString();
    }

    public bool IsNull
    {
        get
        {
            return m_Null;
        }
    }

    public static AccountNr Null
    {
        get
        {
            AccountNr h = new AccountNr();
            h.m_Null = true;
            return h;
        }
    }

    //Method validates account number and assigns bank type. If bank type is unknown then -> 'Bank U/N'
    public bool Validate(string nr) {
        if (nr.Length != 26)
            return false;
        int i = 0;
        Int32 temp = Int32.Parse(nr.Substring(2, 4));
        bool flag = true;
        while (flag==true && i<26) {
            if (Char.IsNumber(nr, i)) { }
            else
                flag = false;
            i++;
        }
        if (i < 26)
            return false;
        if (Enum.IsDefined(typeof(Bank), temp))
        {
            this._bankType = Enum.GetName(typeof(Bank), temp);
        }
        else
            this._bankType = "Bank U/N";
        return true;

    }

    //Method parses SqlString and create object
    public static AccountNr Parse(SqlString s)
    {
        if (s.IsNull)
            return Null;
        AccountNr accountNr = new AccountNr();
        string[] dane = s.Value.Split(",".ToCharArray());

        if (dane.Length != 1) throw new ArgumentException("wprowadz poprawna ilosc argumentów");
        if ( accountNr.Validate(dane[0]) == false) throw new ArgumentException("wprowadz poprawny AccountNr");
    
        accountNr._accountNr = dane[0];

        return accountNr;

    }

    public void Write(System.IO.BinaryWriter w)
    {
        int maxStringSize = 50;
        string paddedString, temp;

        temp = _bankType + "," +_accountNr;
        paddedString = temp.PadRight(maxStringSize, '\0');

        for (int i = 0; i < paddedString.Length; i++)
            w.Write(paddedString[i]);

    }

    public void Read(System.IO.BinaryReader r)
    {
        int maxStringSize = 50;
        char[] chars;
        int stringEnd;
        string stringValue;
        chars = r.ReadChars(maxStringSize);
        stringEnd = Array.IndexOf(chars, '\0');
        if (stringEnd == 0)
        {
            stringValue = null;
            return;
        }
        stringValue = new String(chars, 0, stringEnd);
        string[] dane = stringValue.Split(",".ToCharArray());

        this._accountNr = dane[1];
        this._bankType = dane[0];

    }

}



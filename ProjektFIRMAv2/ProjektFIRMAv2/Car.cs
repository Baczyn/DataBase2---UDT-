using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

[Serializable]
[Microsoft.SqlServer.Server.SqlUserDefinedType(Format.UserDefined, MaxByteSize = 8000, IsByteOrdered = true)]
public struct Car : INullable, IBinarySerialize
{
    //variable for firm of car
    private string _firm;
    //variable for model of car
    private string _model;
    //variable for plate of car
    private string _plate;

    private bool m_Null;

    //constructor
    public Car(string firm, string model, string plate)
    {
        _firm = firm;
        _model = model;
        _plate = plate;
        m_Null = false;

    }
    //constructor
    public Car(bool nothing)
    {
        _model = _firm = "";
        _plate = "";
        m_Null = true;
    }

    public override string ToString()
    {
        return "Car: "+_firm + ", " + _model + ", " + _plate;
    }

    //Method returns plate
    public string GetNr(){
        return _plate;
    }

    public bool IsNull
    {
        get
        {
            return m_Null;
        }
    }

    public static Car Null
    {
        get
        {
            Car h = new Car();
            h.m_Null = true;
            return h;
        }
    }

    //Method parses SqlString and create object
    public static Car Parse(SqlString s)
    {
        if (s.IsNull)
            return Null;
        Car car = new Car();
        string[] data = s.Value.Split(",".ToCharArray());

        if (data.Length != 3) throw new ArgumentException("wprowadz poprawna ilosc argumentów");
        else if (data[0].Length < 1 || data[1].Length < 1 || data[2].Length < 1) throw new ArgumentException("Wprowadz porawna nazwe Modelu i Firmy i nrSeryjnego");
        if (car.Validate(data[2]) == false) throw new ArgumentException("Wprowadz porawna rejestracje");

        return new Car(data[0], data[1], data[2]);

    }
    //Method validats plate (7 chars, first 2 must be upper letters)
    public bool Validate(string plate)
    {
        
        if (plate.Length != 7)
            return false;
        if ((plate[0] >= 'A' && plate[0] <= 'Z')==false)
            return false;
        if ((plate[1] >= 'A' && plate[1] <= 'Z') == false)
            return false;

        return true;
    }

    public void Write(System.IO.BinaryWriter w)
    {
        int maxStringSize = 50;
        string paddedString, temp;

        temp = _firm + "," + _model + "," + _plate;
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
        string[] data = stringValue.Split(",".ToCharArray());

        this._firm = data[0];
        this._model = data[1];
        this._plate = data[2];

    }

}



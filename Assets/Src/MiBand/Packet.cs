using System;

enum Types{
    MESSAGE,
    HEARTRATE
}

[Serializable]
class Packet
{
    public Types type;
    public string value;

    public Packet(Types type, object value){
        this.type = type;
        this.value = value.ToString();
    }

}
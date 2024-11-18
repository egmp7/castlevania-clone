namespace InputCommands.Buttons
{

    public class ButtonA : Button
    {
        public ButtonA()
        {
            inputAction = FindInputAction("ActionA");
        }
    }

    public class ButtonB : Button
    {
        public ButtonB()
        {
            inputAction = FindInputAction("ActionB");
        }
    }

    public class ButtonC : Button
    {
        public ButtonC()
        {
            inputAction = FindInputAction("ActionC");
        }
    }
}

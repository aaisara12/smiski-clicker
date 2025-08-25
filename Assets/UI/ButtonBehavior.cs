
using UnityEngine.UIElements;

[UxmlObject]
public abstract partial class ButtonBehavior
{
    public abstract void OnClicked(Button button);
}


[UxmlObject]
public partial class SellCoinGenerator : ButtonBehavior
{
    public override void OnClicked(Button button)
    {
        int id = int.Parse(button.tooltip);
        GameManager.Instance.SellCoinGenerator(id);
    }
}

[UxmlObject]
public partial class BuyCoinGenerator : ButtonBehavior
{
    public override void OnClicked(Button button)
    {
        int id = int.Parse(button.tooltip);
        GameManager.Instance.BuyCoinGenerator(id);
    }
}

[UxmlObject]
public partial class BuyBanner : ButtonBehavior
{
    public override void OnClicked(Button button)
    {
        int id = int.Parse(button.tooltip);
        GameManager.Instance.BuyBanner(id);
    }
}

[UxmlElement]
public partial class ButtonWithClickBehavior : Button
{
    [UxmlObjectReference("clicked")]
    public ButtonBehavior ClickedBehavior { get; set; }

    public ButtonWithClickBehavior()
    {
        clicked += OnClick;
    }

    void OnClick()
    {
        ClickedBehavior?.OnClicked(this);
    }
}
using JHchoi.Models;
using JHchoi.UI;

namespace JHchoi.Scene
{
    public class TitleScene : IScene
    {
        protected override void OnLoadStart()
        {
            SetResourceLoadComplete();
        }

        protected override void OnLoadComplete()
        {
        }
    }
}

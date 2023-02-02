using NicasourceChallenge.Web._keenthemes.libs;

namespace NicasourceChallenge.Web._keenthemes;

public interface IKTBootstrapBase
{
    void InitThemeMode();
    
    void InitThemeDirection();

    void InitLayout();
    
    void Init(IKTTheme theme);
}
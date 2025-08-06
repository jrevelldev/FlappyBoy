using UnityEngine;

[CreateAssetMenu(fileName = "Sprite Import Tips", menuName = "Tools/Sprite Import Tips")]
public class FolderNote : ScriptableObject
{
    [TextArea(5, 20)]
    public string note =
@"For every pixel art sprite:
1. Select the sprite in the Project window.
2. In the Inspector, set:
- ✅ Texture Type: Sprite (2D and UI)
- ✅ Pixels Per Unit (PPU): 16 or 32 (match your game scale)
- ✅ Filter Mode: Point (no filter) 
- ✅ Compression: None

Repeat for every sprite sheet, background, icon, etc.

📌 Click Apply after each change.";
}

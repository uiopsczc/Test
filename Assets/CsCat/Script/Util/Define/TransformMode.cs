namespace CsCat
{
  public enum TransformMode
  {
    localPosition = 1 << 0,
    localRotation = 1 << 1,
    localScale = 1 << 2,
    position = 1 << 3,
    rotation = 1 << 4,
    scale = 1 << 5
  }
}
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LemonStudios.UI
{
    public static class LemonUIUtils
    {
        public static void SwitchMenus(GameObject menuToHide, GameObject menuToShow)
        {
            menuToHide.SetActive(false);
            menuToShow.SetActive(true);
        }
        
        public static Color CreateNormalizedColor(int r, int g, int b, int a = 255)
        {
            float normalizedR = ColorNormalizer(r);
            float normalizedG = ColorNormalizer(g);
            float normalizedB = ColorNormalizer(b);
            float normalizedA = ColorNormalizer(a);
            
            // Using the new "normalized" values, return a Color class using unity's (frankly stupid) implementation of the RGB colour system
            return new Color(normalizedR, normalizedG, normalizedB, normalizedA);
        }

        private static float ColorNormalizer(int input)
        {
            // Helper method required for CreateNormalizedColor()
            // picks the larger number, 0, or input / 255
            return Mathf.Max(0, Mathf.Min(255, (float) input) / 255);
        }
        
        public static IEnumerator SmoothlyUpdateFillUI(Image targetGraphic, float targetFillAmount, float duration = 1f)
        {
            float startFill = targetGraphic.fillAmount;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float currentFill = Mathf.Lerp(startFill, targetFillAmount, elapsed / duration);
                targetGraphic.fillAmount = currentFill;

                yield return null;
            }

            // Ensure the final fill amount is exactly the target fill amount
            targetGraphic.fillAmount = targetFillAmount;
        }
   
        
        // Use Mathf.Lerp() to smoothly change the alpha of an image
        public static IEnumerator SmoothAlphaUpdate(Image targetGraphic, float targetAlpha, float animationDuration)
        {
            Color graphicColor = targetGraphic.color;
            float originalAlpha = targetGraphic.color.a;
            float currentTime = 0;
            
            while (currentTime < animationDuration) 
            {
                currentTime += Time.deltaTime;
                float newAlpha = Mathf.Lerp(originalAlpha, targetAlpha, currentTime / animationDuration);
                targetGraphic.color = new Color(graphicColor.r, graphicColor.g, graphicColor.b, newAlpha);

                yield return new WaitForEndOfFrame();
            }
        }
        
        
        // Exact same method as above but for TextMeshProUGUI elements instead of an image
        public static IEnumerator SmoothAlphaUpdate(TextMeshProUGUI targetGraphic, float targetAlpha, float animationDuration)
        {
            Color graphicColor = targetGraphic.color;
            float originalAlpha = targetGraphic.color.a;
            float currentTime = 0;
            
            while (currentTime < animationDuration) 
            {
                currentTime += Time.deltaTime;
                float newAlpha = Mathf.Lerp(originalAlpha, targetAlpha, currentTime / animationDuration);
                targetGraphic.color = new Color(graphicColor.r, graphicColor.g, graphicColor.b, newAlpha);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}

// 3) El “gestor” de plataformas:
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private PlatformPool pool;       // Arrastra el PlatformPool aquí
    [SerializeField] private Transform player;        // El transform de tu cubito

    [Header("Configuración de generación")]
    [SerializeField] private int initialPlatforms = 10;
    [SerializeField] private float verticalSpacing = 2.5f;
    [SerializeField] private float horizontalRange = 3f;
    [SerializeField] private float despawnBelowPlayerBy = 5f;

    private float highestY;                            // Altura de la última plataforma generada
    private readonly List<Platform> activePlatforms = new List<Platform>();

    void Start()
    {
        // Generar plataformas iniciales
        highestY = player.position.y - verticalSpacing;
        for (int i = 0; i < initialPlatforms; i++)
            SpawnNext();
    }

    void Update()
    {
        // Si el jugador se acerca a la parte superior de las generadas, genera más
        if (player.position.y + (verticalSpacing * 3) > highestY)
            SpawnNext();

        // Recolectar y devolver al pool las que queden muy abajo
        for (int i = activePlatforms.Count - 1; i >= 0; i--)
        {
            if (activePlatforms[i].transform.position.y + despawnBelowPlayerBy < player.position.y)
            {
                pool.ReturnObject(activePlatforms[i]);
                activePlatforms.RemoveAt(i);
            }
        }
    }

    private void SpawnNext()
    {
        // Obtener del pool
        Platform plat = pool.GetObject();
        // Calcular posición aleatoria
        float x = Random.Range(-horizontalRange, horizontalRange);
        highestY += verticalSpacing;
        plat.transform.position = new Vector3(x, highestY, 0f);
        activePlatforms.Add(plat);
    }
}

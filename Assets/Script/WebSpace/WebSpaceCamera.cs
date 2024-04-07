using UnityEngine;

public class WebSpaceCamera : HiddenFXCamera
{
    [Range(0.1f, 0.5f)]
    public float TimeTillImage;
    private float _SpawnDelay;
    // Start is called before the first frame update
    void Start()
    {
        _FXCameraInit();
        _SpawnDelay = TimeTillImage;
        if (Application.isPlaying)
        {
            for (int i = 0; i < 100; i++)
            {
                var newobj = new GameObject("WebSpaceImage");
                WebSpaceImage ImageComp = newobj.AddComponent<WebSpaceImage>();
                ImageComp._ParentCamera = this;
                newobj.transform.SetParent(this.transform);

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //_Tick();
        _UpdateCamera();
    }

    private void _Tick()
    {
        if (!Application.isPlaying) return;
        _SpawnDelay -= Time.deltaTime;
        if (_SpawnDelay <= 0)
        {
            _SpawnDelay = TimeTillImage;

            var newobj = new GameObject("WebSpaceImage");
            newobj.AddComponent<WebSpaceImage>();
            newobj.transform.SetParent(this.transform);
            newobj.transform.position = this.transform.position + Vector3.down * 10;
            newobj.layer = gameObject.layer;

        }
    }
}

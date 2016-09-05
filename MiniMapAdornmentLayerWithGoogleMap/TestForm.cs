using System;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using ThinkGeo.MapSuite.Core;
using ThinkGeo.MapSuite.DesktopEdition;


namespace  MiniMapAdornmentLayerWithGoogleMap
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            // Set the full extent and the background color
            winformsMap1.MapUnit = GeographyUnit.Meter;
            winformsMap1.CurrentExtent = new RectangleShape(-13939426.6371, 6701997.4056, -7812401.86, 2626987.386962);

            GoogleMapsLayer googleMapLayer = new GoogleMapsLayer();
            LayerOverlay layeroverlay = new LayerOverlay();
            layeroverlay.Layers.Add(googleMapLayer);
            winformsMap1.Overlays.Add(layeroverlay);

            MiniMapAdornmentLayer miniMapLayer = new MiniMapAdornmentLayer();
            miniMapLayer.Location = AdornmentLocation.LowerLeft;
            miniMapLayer.Layers.Add(new BackgroundLayer(winformsMap1.BackgroundOverlay.BackgroundBrush));
            miniMapLayer.Layers.Add(googleMapLayer);

            winformsMap1.AdornmentOverlay.Layers.Add(miniMapLayer);
            winformsMap1.Refresh();
        }

      
        private void winformsMap1_MouseMove(object sender, MouseEventArgs e)
        {
            //Displays the X and Y in screen coordinates.
            statusStrip1.Items["toolStripStatusLabelScreen"].Text = "X:" + e.X + " Y:" + e.Y;

            //Gets the PointShape in world coordinates from screen coordinates.
            PointShape pointShape = ExtentHelper.ToWorldCoordinate(winformsMap1.CurrentExtent, new ScreenPointF(e.X, e.Y), winformsMap1.Width, winformsMap1.Height);

            //Displays world coordinates.
            statusStrip1.Items["toolStripStatusLabelWorld"].Text = "(world) X:" + Math.Round(pointShape.X, 4) + " Y:" + Math.Round(pointShape.Y, 4);
        }
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}

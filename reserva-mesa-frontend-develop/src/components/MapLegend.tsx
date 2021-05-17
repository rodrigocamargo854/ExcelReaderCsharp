import React from "react";

const MapLegend: React.FC = () => {
  return (
    <div className="map-legend">
      <div>
        <div className="map-legend-item">
          <div className="map-legend-available"></div>
          <span>Disponível</span>
        </div>
        <div className="map-legend-item">
          <div className="map-legend-reserved"></div>
          <span>Reservada</span>
        </div>
        <div className="map-legend-item">
          <div className="map-legend-inactive"></div>
          <span>Indisponível</span>
        </div>
      </div>
    </div>
  );
}

export default MapLegend;

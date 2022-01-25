import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import Feature from 'ol/Feature';
import Map from 'ol/Map';
import Point from 'ol/geom/Point';
import Projection from 'ol/proj/Projection';
import View from 'ol/View';
import { getCenter } from 'ol/extent';
import ImageLayer from 'ol/layer/Image';
import Static from 'ol/source/ImageStatic';
import { MapService } from '../../services/map.service';
import { CityModel } from '../../models';
import VectorLayer from 'ol/layer/Vector';
import VectorSource from 'ol/source/Vector';
import LineString from 'ol/geom/LineString';
import { Stroke, Style, Icon, Text } from 'ol/style';
import {
  Pointer as PointerInteraction,
  defaults as defaultInteractions,
} from 'ol/interaction';
import { GamesService } from '../../services';
import BaseLayer from 'ol/layer/Base';
import { ConnectionModel } from '../../models/connection.model';

@Component({
  selector: 'catch-me-map-container',
  templateUrl: './map-container.component.html',
  styleUrls: ['./map-container.component.css'],
})
export class MapContainerComponent implements OnInit {
  map!: Map;
  cities!: CityModel[];
  connections!: ConnectionModel[];
  temporaryLayers: BaseLayer[] = [];
  currentCity!: CityModel;

  @Output() moveError = new EventEmitter();

  constructor(
    private mapService: MapService,
    private gamesService: GamesService
  ) {}

  ngOnInit(): void {
    this.map = this.createMap(this.handleDownEvent);

    this.mapService.getCities().subscribe((cities) => {
      this.cities = cities;
      this.currentCity = cities[0];
      this.gamesService.cities = this.cities;
      this.mapService.getConnections().subscribe((connections) => {
        this.connections = connections;
        this.regenerate([this.currentCity.x, this.currentCity.y]);
      });
    });

    this.gamesService.regenerateMap.subscribe((feature) =>
      this.regenerate(feature.values_.geometry.flatCoordinates)
    );

    this.gamesService.setCurrentCity.subscribe((newCity) => {
      if (newCity) {
        if (
          this.connections.filter((conn) => {
            return (
              (conn.cityAId === this.currentCity.id &&
                conn.cityBId === newCity.id) ||
              (conn.cityBId === this.currentCity.id &&
                conn.cityAId === newCity.id)
            );
          }).length > 0
        ) {
          this.currentCity = newCity;
        } else {
          this.moveError.emit('Oda nem vezet út!');
        }
      }

      this.regenerate([this.currentCity.x, this.currentCity.y]);
    });
  }

  private regenerate(playerPos: number[]) {
    this.temporaryLayers.forEach((layer) => this.map.removeLayer(layer));

    createCityLayers(this.cities).forEach((layer) => {
      this.map.addLayer(layer);
      this.temporaryLayers.push(layer);
    });

    this.createConnectionLayers(this.connections).forEach((layer) => {
      this.temporaryLayers.push(layer);
      this.map.addLayer(layer);
    });

    const playerLayer = new VectorLayer({
      source: new VectorSource({
        features: [
          new Feature({
            geometry: new Point([playerPos[0], playerPos[1] + 150], 'XY'),
          }),
        ],
      }),
      style: new Style({
        image: new Icon({
          anchor: [0.5, 0.5],
          anchorXUnits: 'fraction',
          anchorYUnits: 'fraction',
          src: 'assets/img/pieceRed.png',
        }),
      }),
    });

    this.temporaryLayers.push(playerLayer);
    this.map.addLayer(playerLayer);
  }

  aronHelp = '';
  private handleDownEvent(evt: { map: any; pixel: any }) {
    const map = evt.map;

    const feature = map.forEachFeatureAtPixel(evt.pixel, (feature: any) => {
      return feature;
    });

    if (feature) {
      this.gamesService.regenerateMap.emit(feature);
      this.gamesService.setCurrentCity.emit(
        this.gamesService.cities?.find((city) => {
          return city.id === feature.values_.id;
        })
      );

      if (this.aronHelp) {
        this.aronHelp = '';
      } else {
        this.aronHelp = `{ type: 2, cityAId: ${feature.values_.id}, cityBId: `;
      }
    }

    return !!feature;
  }

  private createMap(handleDownEvent: any): Map {
    const extent = [0, 0, 12800, 13824];
    const projection = new Projection({
      code: 'xkcd-image',
      units: 'pixels',
      extent: extent,
    });

    return new Map({
      view: new View({
        projection: projection,
        center: getCenter(extent),
        zoom: 3.5,
        maxZoom: 5,
        minZoom: 3.5,
      }),
      layers: [
        new ImageLayer({
          source: new Static({
            url: 'assets/img/europe.png',
            projection: projection,
            imageExtent: extent,
          }),
        }),
      ],
      interactions: defaultInteractions().extend([
        new MouseDown(this.gamesService, handleDownEvent),
      ]),
    });
  }

  private createConnectionLayers(connections: any[]): any[] {
    const createLayer = (connections: any[], color: string) => {
      return new VectorLayer({
        source: new VectorSource({
          features: connections.map((connection) => {
            const cityA = this.cities.filter(
              (f) => f.id == connection.cityAId
            )[0];
            const cityB = this.cities.filter(
              (f) => f.id == connection.cityBId
            )[0];
            return new Feature(
              new LineString([
                [cityA.x, cityA.y],
                [cityB.x, cityB.y],
              ])
            );
          }),
        }),
        style: new Style({
          stroke: new Stroke({
            color: color,
            width: 3,
          }),
        }),
      });
    };

    return [
      createLayer(
        connections.filter((connection) => connection.type == 1),
        '#3218e1'
      ),
      createLayer(
        connections.filter((connection) => connection.type == 2),
        '#1bac74'
      ),
      createLayer(
        connections.filter((connection) => connection.type == 3),
        '#e42426'
      ),
    ];
  }
}

function createCityLayers(cities: CityModel[]): any[] {
  const layers = [
    new VectorLayer({
      source: new VectorSource({
        features: cities.map((city) => {
          return new Feature({
            geometry: new Point([city.x, city.y], 'XY'),
            name: city.name,
            id: city.id,
          });
        }),
      }),
      style: new Style({
        image: new Icon({
          anchor: [0.5, 0.5],
          anchorXUnits: 'fraction',
          anchorYUnits: 'fraction',
          src: 'assets/img/threeStations.png',
        }),
      }),
    }),
  ];

  cities
    .map((city) => {
      return new VectorLayer({
        source: new VectorSource({
          features: [new Feature(new Point([city.x, city.y + 120], 'XY'))],
        }),
        style: new Style({
          text: new Text({
            text: city.name,
            scale: 1.7,
          }),
        }),
      });
    })
    .forEach((layer) => layers.push(layer));

  return layers;
}

class MouseDown extends PointerInteraction {
  constructor(
    private gamesService: GamesService,
    handleDownEvent: (evt: any) => boolean
  ) {
    super({
      handleDownEvent: handleDownEvent,
    });
  }
}

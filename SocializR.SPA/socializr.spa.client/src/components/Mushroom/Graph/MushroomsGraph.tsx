import { useEffect } from "react";
import Graph from "graphology";
import { SigmaContainer, useLoadGraph } from "@react-sigma/core";
import "@react-sigma/core/lib/react-sigma.min.css";
import mushroomsService from "../../../services/mushrooms.service";
import React from "react";
import { MushroomGraph } from "../../../types/types";
import { useWorkerLayoutForceAtlas2 } from "@react-sigma/layout-forceatlas2";

export const getRandomInt = (min: number, max: number) => {
  min = Math.ceil(min);
  max = Math.floor(max);
  return Math.floor(Math.random() * (max - min + 1)) + min;
}

function getColor(id: number) {
  if (id >= 12 && id <= 77) return "yellow";
  else if (id > 77 && id <= 139) return "red";
  else return "blue";
}

export const LoadGraph = () => {
  const loadGraph = useLoadGraph();
  const [data, setData] = React.useState<MushroomGraph>();

  const fetchGraphData = async () => {
    try {
      const mushroomGraph = await mushroomsService.getGraph();
      setData(mushroomGraph);
    } catch (err) {
      console.log(err);
    }
  };

  useEffect(() => {
    fetchGraphData();
  }, []);

  useEffect(() => {
    const graph = new Graph();

    data?.nodes.forEach(node => {
      graph.addNode(node.id,
        {
          x: getRandomInt(1, 500),
          y: getRandomInt(1, 500),
          label: node.name,
          size: 2 * data.edges.filter(e => e.to === node.id || e.from === node.id).length,
          color: getColor(node.id),
        });

    });

    data?.edges.forEach(edge => {
      graph.addDirectedEdge(edge.from, edge.to, { label: "seamana cu" });
    });

    loadGraph(graph);
  }, [loadGraph, data]);

  return null;
};

export const MushroomsGraph = () => {
  const Fa2 = () => {
    const { start, kill } = useWorkerLayoutForceAtlas2({ settings: { slowDown: 100, gravity: 25 } });

    useEffect(() => {
      start();
      return () => {
        kill();
      };
    }, [start, kill]);

    return null;
  };

  return (
    <SigmaContainer style={{ height: "100%", width: "100%" }}>
      <LoadGraph />
      <Fa2></Fa2>
    </SigmaContainer>
  );
};
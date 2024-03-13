import { useEffect, useState } from "react";
import { Button, Col, Container, Row } from "react-bootstrap";
import { getRandomInt } from "./Graph/MushroomsGraph";
import mushroomsService from "../../services/mushrooms.service";
import GameCard from "./GameCard";
import { Ciuperca, Comestibilitate, GameStatus, MorfologieCorpFructifer } from "../../types/types";
import MushroomName from "./MushroomName";
import Graph from "graphology";
import { bidirectional } from "graphology-shortest-path";

const defaultValue = {
    id: 0,
    denumire: "",
    denumirePopulara: "",
    corpulFructifer: "",
    ramurile: "",
    palaria: "",
    piciorul: "",
    stratulHimenial: "",
    gleba: "",
    tuburileSporifere: "",
    lamelele: "",
    carnea: "",
    perioadaDeAparitie: "",
    valoareaAlimentara: "",
    speciiAsemanatoare: "",
    idSpeciiAsemanatoare: [],
    esteMedicinala: false,
    comestibilitate: Comestibilitate.Necomestibila,
    locDeFructificatie: [],
    morfologieCorpFructifer: MorfologieCorpFructifer.HimenoforNelamelarNetubular,
    luniDeAparitie: [],
    perioada: [],
}

const Game = () => {
    const [graph, setGraph] = useState(new Graph());
    const [ids, setIds] = useState([304, 305]);
    const [mushrooms, setMushrooms] = useState<Ciuperca[]>([defaultValue, defaultValue]);
    const [selectedMushroom, setSelectedMushroom] = useState<Ciuperca>(defaultValue);
    const [gameState, setGameState] = useState(GameStatus.Playing);
    const [clickedId, setClickedId] = useState(0);
    const [score, setScore] = useState(0);
    const [difficulty, setDifficulty] = useState<number>();

    const fetchGraphData = async () => {
        const graph = new Graph();
        try {
            const data = await mushroomsService.getGraph();
            data?.nodes.forEach(node => {
                graph.addNode(node.id,
                    {
                        x: 0,
                        y: 0,
                        label: node.name
                    });

            });

            data?.edges.forEach(edge => {
                graph.addDirectedEdge(edge.from, edge.to);
            });

        } catch (err) {
            console.log(err);
        }

        setGraph(graph);
    };

    async function fetchMushrooms() {
        try {
            var result = await mushroomsService.getMushrooms(ids);
            console.log(result);
            setMushrooms(result);
            setSelectedMushroom(result[getRandomInt(0, 1)]);
        } catch (error) {
            console.error(error);
        }
    };

    const calculateDifficulty = (): number => {
        let shortestPath = bidirectional(graph, ids[0], ids[1]);
        if(shortestPath === null) shortestPath = bidirectional(graph, ids[1], ids[0]);
        return shortestPath?.length ?? 0;
    };

    useEffect(() => {
        fetchGraphData();
    }, []);

    useEffect(() => {
        if (ids[0] === ids[1]) {
            setIds([getRandomInt(12, 372), getRandomInt(12, 372)]);
        } else {
            fetchMushrooms();
        }
    }, [ids]);

    useEffect(() => {
        if (graph.size > 0) {
            setDifficulty(calculateDifficulty());
        }
    }, [ids, graph])

    const handleButtonClick = (clickedId: number) => {
        setClickedId(clickedId);
        if (clickedId === selectedMushroom.id) {
            setGameState(GameStatus.Won);
        } else {
            setGameState(GameStatus.Lost);
        }
    };

    const handleResetGame = () => {
        setIds([getRandomInt(12, 372), getRandomInt(12, 372)]);
        setGameState(GameStatus.Playing);
        setSelectedMushroom(defaultValue);
        setClickedId(0);
    };

    return (
        <Container className="text-center">
            <span className="float-ebd">Difficulty: {difficulty}</span>
            {gameState == GameStatus.Playing &&
                <>
                    <p>
                        Selecteaza din variantele de mai jos următoarea ciupercă
                    </p>
                    <h5 className="kaki mb-4">
                        <MushroomName denumire={selectedMushroom?.denumire} denumirePopulara={selectedMushroom?.denumirePopulara}>
                        </MushroomName>
                    </h5>
                </>
            }
            {gameState == GameStatus.Won && <h5 className="text-success fw-bold">Felicitări! Răspuns corect.</h5>}
            {gameState == GameStatus.Lost && <h5 className="text-danger fw-bold">Răspuns greșit.</h5>}
            {gameState != GameStatus.Playing &&
                <Button onClick={handleResetGame}>Next</Button>
            }
            <Row>
                <h5></h5>
                {mushrooms.map(m => <Col><GameCard key={m.id} mushroom={m} onClick={handleButtonClick} gameStatus={gameState} clickedId={clickedId}></GameCard></Col>)}
            </Row>
        </Container>
    );
};

export default Game;
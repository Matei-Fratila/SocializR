import { useCallback, useEffect, useState } from "react";
import { Button, Col, Container, Row } from "react-bootstrap";
import { getRandomInt } from "./Graph/MushroomsGraph";
import mushroomsService from "../../services/mushrooms.service";
import GameCard from "./GameCard";
import { Ciuperca, Comestibilitate, Difficulty, EndGameModel, GameStatus, GameType, MorfologieCorpFructifer } from "../../types/types";
import MushroomName from "./MushroomName";
import Graph from "graphology";
import { bidirectional } from "graphology-shortest-path";
import { HeartFill } from "react-bootstrap-icons";
import gameService from "../../services/game.service";

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
};

const defaultGameInfo = {
    sessionId: "",
    remainingNumberOfHearts: 0,
    score: 0,
    gameType: GameType.MatchNameToPicture,
    numberOfCorrectEasyQuestions: 0,
    numberOfCorrectMediumQuestions: 0,
    numberOfCorrectHardQuestions: 0,
    numberOfCorrectExpertQuestions: 0,
    numberOfIncorrectEasyQuestions: 0,
    numberOfIncorrectMediumQuestions: 0,
    numberOfIncorrectHardQuestions: 0,
    numberOfIncorrectExpertQuestions: 0,
};

const Dificultate = ["Ușor", "Mediu", "Avansat", "Expert"];

const Game = () => {
    const [graph, setGraph] = useState(new Graph());
    const [ids, setIds] = useState([getRandomInt(12, 372), getRandomInt(12, 372)]);
    const [mushrooms, setMushrooms] = useState<Ciuperca[]>([defaultValue, defaultValue]);
    const [selectedMushroom, setSelectedMushroom] = useState<Ciuperca>(defaultValue);
    const [gameState, setGameState] = useState(GameStatus.Playing);
    const [clickedId, setClickedId] = useState(0);
    const [pathLength, setPathLength] = useState(0);
    const [score, setScore] = useState(0);
    const [difficulty, setDifficulty] = useState<Difficulty>(Difficulty.Easy);
    const [hearts, setHearts] = useState<number>(5);
    const [maxHearts, setMaxHearts] = useState<number>(0);
    const [gameInfo, setGameInfo] = useState<EndGameModel>(defaultGameInfo);

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
                if (!graph.hasEdge(edge.from, edge.to))
                    graph.addDirectedEdge(edge.from, edge.to);
                if (!graph.hasEdge(edge.to, edge.from))
                    graph.addDirectedEdge(edge.to, edge.from);
            });

        } catch (err) {
            console.log(err);
        }

        setGraph(graph);
    };

    const startGame = async () => {
        try {
            const game = await gameService.startGame();
            setHearts(game.usersNumberOfHearts);
            setMaxHearts(game.maxNumberOfHearts);
            setScore(game.score);
        } catch (err) {
            console.log(err);
        }
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

    const getPathLength = (): number => {
        let shortestPath = bidirectional(graph, ids[0], ids[1]);
        if (shortestPath === null) shortestPath = bidirectional(graph, ids[1], ids[0]);
        return shortestPath?.length ?? 0;
    };

    useEffect(() => {
        fetchGraphData();
        startGame();
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
            setPathLength(getPathLength());
        }
    }, [ids, graph]);

    useEffect(() => {
        setDifficulty(calculateDifficulty());
    }, [mushrooms]);

    useEffect(() => {
        if (hearts === 0) {
            setGameState(GameStatus.Ended);
        };
    }, [hearts]);

    const handleButtonClick = async (clickedId: number) => {
        setClickedId(clickedId);
        if (clickedId === selectedMushroom.id) {
            setGameState(GameStatus.Won);
            setScore(score + calculateScore());
            updateGameInfo(true);
            const hearts = await gameService.submitAnswer(difficulty, true, calculateScore());
            setHearts(hearts);
        } else {
            setGameState(GameStatus.Lost);
            setScore(score - calculateScore());
            const hearts = await gameService.submitAnswer(difficulty, false, -calculateScore());
            setHearts(hearts);
            updateGameInfo(false);
        }
    };

    const handleResetGame = () => {
        setIds([getRandomInt(12, 372), getRandomInt(12, 372)]);
        setGameState(GameStatus.Playing);
        setSelectedMushroom(defaultValue);
        setClickedId(0);
    };

    function calculateScore(): number {
        switch (difficulty) {
            case Difficulty.Easy: return 1;
            case Difficulty.Medium: return 2;
            case Difficulty.Hard: return 8 - pathLength;
            case Difficulty.Expert: return 10;
            default: return 0;
        };
    };

    function calculateDifficulty(): Difficulty {
        const haveSameMorphology = mushrooms[0].morfologieCorpFructifer === mushrooms[1].morfologieCorpFructifer;
        const inSameGenus = mushrooms[0].denumire.split(' ')[0] === mushrooms[1].denumire.split(' ')[0];

        switch (true) {
            case (pathLength === 0): {
                if (inSameGenus) {
                    return Difficulty.Hard;
                } else if (haveSameMorphology) {
                    return Difficulty.Medium;
                }
                else return Difficulty.Easy;
            }
            case (pathLength === 1): {
                if (inSameGenus) {
                    return Difficulty.Expert;
                } else return Difficulty.Hard;
            }
            case (pathLength <= 5): {
                if (inSameGenus) {
                    return Difficulty.Hard;
                }
                else return Difficulty.Medium;
            }
            default: {
                if (inSameGenus) {
                    return Difficulty.Hard;
                } else if (haveSameMorphology) {
                    return Difficulty.Medium;
                }
                return Difficulty.Easy;
            }
        }
    };

    function getBadgeColor() {
        switch (difficulty) {
            case Difficulty.Easy: return "bg-success";
            case Difficulty.Medium: return "bg-primary";
            case Difficulty.Hard: return "bg-warning";
            case Difficulty.Expert: return "bg-danger";
        };
    };

    function displayhearts() {
        const icons = [];
        for (let i = 0; i < maxHearts; i++) {
            if (i < hearts) icons.push(<HeartFill color="red" className="me-1"></HeartFill>);
            else icons.push(<HeartFill color="gray" className="me-1"></HeartFill>);
        };
        return icons;
    };

    function updateGameInfo(hasWon: boolean) {
        switch (difficulty) {
            case Difficulty.Easy: {
                hasWon
                    ? setGameInfo({ ...gameInfo, numberOfCorrectEasyQuestions: gameInfo.numberOfCorrectEasyQuestions + 1 })
                    : setGameInfo({ ...gameInfo, numberOfIncorrectEasyQuestions: gameInfo.numberOfIncorrectEasyQuestions + 1 });
                return;
            }
            case Difficulty.Medium: {
                hasWon
                    ? setGameInfo({ ...gameInfo, numberOfCorrectMediumQuestions: gameInfo.numberOfCorrectMediumQuestions + 1 })
                    : setGameInfo({ ...gameInfo, numberOfIncorrectMediumQuestions: gameInfo.numberOfIncorrectMediumQuestions + 1 });
                return;
            }
            case Difficulty.Hard: {
                hasWon
                    ? setGameInfo({ ...gameInfo, numberOfCorrectHardQuestions: gameInfo.numberOfCorrectHardQuestions + 1 })
                    : setGameInfo({ ...gameInfo, numberOfIncorrectHardQuestions: gameInfo.numberOfIncorrectHardQuestions + 1 });
                return;
            }
            case Difficulty.Expert: {
                hasWon
                    ? setGameInfo({ ...gameInfo, numberOfCorrectExpertQuestions: gameInfo.numberOfCorrectExpertQuestions + 1 })
                    : setGameInfo({ ...gameInfo, numberOfIncorrectExpertQuestions: gameInfo.numberOfIncorrectExpertQuestions + 1 });
                return;
            }
        };
    }

    return (
        <Container className="text-center">
            <span className="fw-bold float-start">Scor: {score}</span>
            <span className="fw-bold float-end">
                {displayhearts()}
            </span><br></br>
            {gameState == GameStatus.Playing &&
                <>
                    <p>
                        Selecteaza din variantele de mai jos următoarea ciupercă
                    </p>
                    <h5 className="kaki">
                        <MushroomName denumire={selectedMushroom?.denumire} denumirePopulara={selectedMushroom?.denumirePopulara}>
                        </MushroomName>
                    </h5>
                    <span className={`badge ${getBadgeColor()}`}>{Dificultate[difficulty]}</span>
                </>
            }
            {gameState == GameStatus.Won && <h5 className="text-success fw-bold">Felicitări! Răspuns corect.</h5>}
            {gameState == GameStatus.Lost && <h5 className="text-danger fw-bold">Răspuns greșit.</h5>}
            {(gameState == GameStatus.Lost || gameState == GameStatus.Won) &&
                <Button onClick={handleResetGame}>Next</Button>
            }
            {gameState == GameStatus.Ended &&
                <h5 className="kaki">Momentan nu mai ai încercări. Încearcă mai târziu</h5>
            }
            {gameState != GameStatus.Ended &&
                <Row>
                    <h5></h5>
                    {mushrooms.map(m => <Col><GameCard key={m.id} mushroom={m} onClick={handleButtonClick} gameStatus={gameState} clickedId={clickedId}></GameCard></Col>)}
                </Row>
            }
        </Container>
    );
};

export default Game;
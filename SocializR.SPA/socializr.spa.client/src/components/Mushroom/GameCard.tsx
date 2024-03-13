import './Mushroom.css';
import { Card, CardBody, CardText, CardTitle } from "react-bootstrap";
import { Ciuperca, GameStatus } from "../../types/types";
import mushroomsService from "../../services/mushrooms.service";
import Calendar from "./Calendar";
import MushroomInfo from "./MushroomInfo";
import MushroomName from './MushroomName';

interface GameCardProps {
    mushroom: Ciuperca,
    onClick: (id: number) => void,
    gameStatus: GameStatus,
    clickedId: number
}

const GameCard = ({ mushroom, onClick, gameStatus, clickedId }: GameCardProps) => {

    const currentMonth = new Date().getMonth() + 1;
    const isInSeason = currentMonth >= mushroom?.perioada[0] && currentMonth <= mushroom?.perioada[1];
    const amrTillInSeason = Math.min(...mushroom?.perioada) - currentMonth;
    const amrOfSeason = Math.abs(mushroom?.perioada[0] - mushroom?.perioada[1]);

    return (
        <Card className={
            `btn p-0 shadow mushroom-card
            ${(gameStatus == GameStatus.Playing) ? 'btn-light enabled' : ''}
            ${(gameStatus == GameStatus.Won && mushroom.id === clickedId) ? 'btn-success' : ''}
            ${(gameStatus == GameStatus.Lost && mushroom.id === clickedId) ? 'btn-danger' : ''}
            ${(gameStatus != GameStatus.Playing && mushroom.id !== clickedId) ? 'btn-light disabled' : ''}
            `}
            onClick={e => onClick(mushroom.id)}>

            <Card.Img variant="top"
                src={`${mushroomsService.mushroomsApi.defaults.baseURL}/images/${mushroom?.id}.jpg`} className="px-0">
            </Card.Img>

            <MushroomInfo {...mushroom} idSpeciiAsemanatoare={[]}></MushroomInfo>

            <CardBody>
                <CardTitle className="kaki mb-2">
                    {gameStatus != GameStatus.Playing && <MushroomName {...mushroom}></MushroomName>}
                    <span title={isInSeason ? `în sezon pentru încă ${amrOfSeason} luni` : `mai ai de așteptat ${amrTillInSeason} luni`} className={`badge rounded-pill ${isInSeason ? 'bg-success' : 'bg-danger'}`}>
                        {isInSeason ? 'în sezon' : 'nu e în sezon'}
                    </span>
                </CardTitle>
                {mushroom?.corpulFructifer &&
                    <CardText>
                        <span>
                            Corpul fructifer:{" "}
                        </span>
                        {mushroom.corpulFructifer}
                    </CardText>
                }
                {mushroom?.palaria &&
                    <CardText>
                        <span>
                            Pălăria:{" "}
                        </span>
                        {mushroom.palaria}
                    </CardText>
                }
                {mushroom?.tuburileSporifere &&
                    <CardText>
                        <span>
                            Tuburile sporifere:{" "}
                        </span>
                        {mushroom.tuburileSporifere}
                    </CardText>
                }
                {mushroom?.lamelele &&
                    <CardText>
                        <span>
                            Lamelele:{" "}
                        </span>
                        {mushroom.lamelele}
                    </CardText>
                }
                {mushroom?.stratulHimenial &&
                    <CardText>
                        <span>
                            Stratul himenial:{" "}
                        </span>
                        {mushroom.stratulHimenial}
                    </CardText>}
                {mushroom?.gleba &&
                    <CardText>
                        <span>
                            Gleba:{" "}
                        </span>
                        {mushroom.gleba}
                    </CardText>
                }
                {mushroom?.piciorul &&
                    <CardText>
                        <span>
                            Piciorul:{" "}
                        </span>
                        {mushroom.piciorul}
                    </CardText>
                }
                {mushroom?.carnea &&
                    <CardText>
                        <span>
                            Carnea:{" "}
                        </span>
                        {mushroom.carnea}
                    </CardText>
                }
                {mushroom?.valoareaAlimentara &&
                    <CardText>
                        <span>
                            Valoarea alimentară:{" "}
                        </span>
                        {mushroom.valoareaAlimentara}
                    </CardText>
                }
                <CardText title={`pagina ${mushroom?.id} din Ghidul Ciupercarului`}><span className="float-end">{mushroom?.id}</span></CardText>
            </CardBody>

            <Card.Footer>
                <Calendar perioada={mushroom?.perioada}></Calendar>
            </Card.Footer>
        </Card>
    )
};

export default GameCard;
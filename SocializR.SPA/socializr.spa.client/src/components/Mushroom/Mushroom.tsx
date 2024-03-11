import React from "react";
import './Mushroom.css';
import { Card, CardBody, CardText, CardTitle } from "react-bootstrap";
import { Ciuperca, Comestibilitate, MorfologieCorpFructifer } from "../../types/types";
import mushroomsService from "../../services/mushrooms.service";
import { Link, useParams } from "react-router-dom";
import Calendar from "./Calendar";
import MushroomInfo from "./MushroomInfo";
import { PencilFill } from "react-bootstrap-icons";
import MushroomName from "./MushroomName";

interface MushroomProps {
    mushroom: Ciuperca
}

const Mushroom = (props: MushroomProps | any) => {
    const { id } = useParams();

    const [mushroom, setMushroom] = React.useState<Ciuperca>(props?.mushroom ?? {
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
    });

    const handleFetchMushroom = React.useCallback(async () => {
        try {
            const result = await mushroomsService.getMushroom(Number(id));
            setMushroom(result);
        }
        catch {
    }
}, [id]);

React.useEffect(() => {
    if (id !== undefined) {
        handleFetchMushroom();
    }
}, [id]);

const currentMonth = new Date().getMonth() + 1;
const isInSeason = currentMonth >= mushroom.perioada[0] && currentMonth <= mushroom.perioada[1];
const amrTillInSeason = Math.min(...mushroom.perioada) - currentMonth;
const amrOfSeason = Math.abs(mushroom.perioada[0] - mushroom.perioada[1]);
const mushroomId = id !== undefined ? Number(id) : mushroom.id;

return (
    <Card className="shadow mushroom-card">
        <Link to={`/mushrooms/${mushroomId}`}>
            <Card.Img variant="top" title={`află mai multe informații despre ${mushroom.denumire}`}
                src={`${mushroomsService.mushroomsApi.defaults.baseURL}/images/${mushroomId}.jpg`} className="px-0">
            </Card.Img>
        </Link>

        <MushroomInfo {...mushroom}></MushroomInfo>

        <CardBody>
            <CardTitle className="kaki mb-2">
                <MushroomName {...mushroom}></MushroomName>
                <Link to={`/mushrooms/edit/${mushroomId}`}>
                    <PencilFill title="editeaza informația" className="kaki"></PencilFill>
                </Link>
                {" "}
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
                </CardText>}
            {mushroom?.perioadaDeAparitie &&
                <CardText>
                    <span>
                        Perioada de apariție:{" "}
                    </span>
                    {mushroom.perioadaDeAparitie}
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
            {mushroom?.speciiAsemanatoare &&
                <CardText>
                    <span>
                        Specii asemănătoare:{" "}
                    </span>
                    {mushroom.speciiAsemanatoare}

                </CardText>
            }
            <CardText title={`pagina ${mushroom.id} din Ghidul Ciupercarului`}><span className="float-end">{mushroom.id}</span></CardText>
        </CardBody>

        <Card.Footer>
            <Calendar perioada={mushroom.perioada}></Calendar>
        </Card.Footer>
    </Card>
)
};

export default Mushroom;
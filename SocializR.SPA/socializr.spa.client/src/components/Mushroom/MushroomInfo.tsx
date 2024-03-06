import DeciduousForest from "./svgs/DeciduousForest";
import ConiferousForest from "./svgs/ConiferousForest";
import TreeStump from "./svgs/TreeStump";
import Grass from "./svgs/Grass";
import Hand from "./svgs/Hand";
import Medicine from "./svgs/Medicine";
import Skull from "./svgs/Skull";
import ForkAndSpoon from "./svgs/ForkAndSpoon";
import ExclamationForkAndSpoon from "./svgs/ExclamationForkAndSpoon";
import CrossedForkAndSpoon from "./svgs/CrossedForkAndSpoon";
import './MushroomInfo.css'
import { Button, Col } from "react-bootstrap";
import { Link } from "react-router-dom";
import { Comestibilitate, LocDeFructificatie, MorfologieCorpFructifer } from "../../types/types";

interface MushroomInfoProps {
    id: number,
    idSpeciiAsemanatoare: number[],
    esteMedicinala: boolean,
    comestibilitate: Comestibilitate,
    locDeFructificatie: LocDeFructificatie[],
    morfologieCorpFructifer: MorfologieCorpFructifer
}

const buttonStyle = {
    width: "51%",
    height: "auto"
}

const MushroomInfo = (props: MushroomInfoProps) => {
    const { idSpeciiAsemanatoare, esteMedicinala, comestibilitate, locDeFructificatie, morfologieCorpFructifer } = props;

    const renderComestibilitate = () => {
        switch (comestibilitate) {
            case Comestibilitate.Necunoscuta:
                return "";
            case Comestibilitate.Comestibila:
                return (<ForkAndSpoon></ForkAndSpoon>);
            case Comestibilitate.ConditionatComestibila:
                return (<ExclamationForkAndSpoon></ExclamationForkAndSpoon>);
            case Comestibilitate.Necomestibila:
                return (<CrossedForkAndSpoon></CrossedForkAndSpoon>);
            case Comestibilitate.Otravitoare:
                return (<Skull></Skull>);
            default:
                return "";
        }
    };

    function renderLocDeFructificatie(loc: LocDeFructificatie) {
        switch (loc) {
            case LocDeFructificatie.PadureFoioase:
                return (<DeciduousForest></DeciduousForest>);
            case LocDeFructificatie.PadureConifere:
                return (<ConiferousForest></ConiferousForest>);
            case LocDeFructificatie.Pasune:
                return (<Grass></Grass>);
            case LocDeFructificatie.CrengiSiCioate:
                return (<TreeStump></TreeStump>);
            default:
                return "";
        }
    };

    function colorMushroomInfo(morfo: MorfologieCorpFructifer): string {
        switch (morfo) {
            case MorfologieCorpFructifer.HimenoforNelamelarNetubular:
                return 'bg-himenofor-nelamelar-netubular';
            case MorfologieCorpFructifer.HimenoforTubular:
                return 'bg-himenofor-tubular';
            case MorfologieCorpFructifer.HimenoforLamelar:
                return 'bg-himenofor-lamelar';
            default: return '';
        }
    };

    return (
        <Col lg={2} md={2} sm={2} xs={12} className={`${colorMushroomInfo(morfologieCorpFructifer)} text-center`}>
            {renderComestibilitate() !== "" && <Button style={buttonStyle} variant="light mt-3 border border-dark">
                {renderComestibilitate()}
            </Button>}

            {esteMedicinala &&
                <Button style={buttonStyle} variant="light mt-3 border border-dark">
                    <Medicine></Medicine>
                </Button>
            }

            {locDeFructificatie && locDeFructificatie?.length !== 0 &&
                locDeFructificatie?.map(loc =>
                    <Button style={buttonStyle} variant="light mt-3 border border-dark">{
                        renderLocDeFructificatie(loc)}
                    </Button>)
            }

            {idSpeciiAsemanatoare && idSpeciiAsemanatoare?.length !== 0 &&
                <>
                    <Button style={buttonStyle} variant="light my-3 border border-dark">
                        <Hand></Hand>
                    </Button>
                    {idSpeciiAsemanatoare.map(i =>
                        <Link key={i} to={`/mushrooms/${i}`}>
                            <Button style={buttonStyle} variant={`light border border-dark mb-3`}>
                                <span className="fw-bold">{i}</span>
                            </Button>
                        </Link>
                    )}
                </>
            }
        </Col>
    );
};

export default MushroomInfo;
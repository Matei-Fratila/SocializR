import DeciduousForest from "./svgs/DeciduousForest";
import ConiferousForest from "./svgs/ConiferousForest";
import TreeStump from "./svgs/TreeStump";
import Grass from "./svgs/Grass";
import Medicine from "./svgs/Medicine";
import Skull from "./svgs/Skull";
import ForkAndSpoon from "./svgs/ForkAndSpoon";
import ExclamationForkAndSpoon from "./svgs/ExclamationForkAndSpoon";
import CrossedForkAndSpoon from "./svgs/CrossedForkAndSpoon";
import './MushroomInfo.css'
import { Button, ButtonGroup, CardHeader } from "react-bootstrap";
import { Comestibilitate, LocDeFructificatie, MorfologieCorpFructifer } from "../../types/types";
import { useNavigate } from "react-router-dom";
import splitCamelCase from "../../helpers/string-helper";
import { Popover } from "@mui/material";
import React from "react";
import MushroomPopover from "./MushroomPopover";


interface MushroomInfoProps {
    id: number,
    idSpeciiAsemanatoare: number[],
    esteMedicinala: boolean,
    comestibilitate: Comestibilitate,
    locDeFructificatie: LocDeFructificatie[],
    morfologieCorpFructifer: MorfologieCorpFructifer,
    perioada: number[]
}

const buttonStyle = {
    width: "4em",
    height: "3em",
}

const MushroomInfo = ({
    idSpeciiAsemanatoare,
    esteMedicinala,
    comestibilitate,
    locDeFructificatie,
    morfologieCorpFructifer }: MushroomInfoProps) => {

    const navigate = useNavigate();
    const [anchorEl, setAnchorEl] = React.useState<HTMLElement | null>(null);

    const handlePopoverOpen = (event: React.MouseEvent<HTMLElement>) => {
        setAnchorEl(event.currentTarget);
    };

    const handlePopoverClose = () => {
        setAnchorEl(null);
    };

    const open = Boolean(anchorEl);

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

    function colorMushroomInfo(): string {
        switch (morfologieCorpFructifer) {
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
        <CardHeader className={colorMushroomInfo()} title={`prezintă ${splitCamelCase(morfologieCorpFructifer).toLocaleLowerCase()}`}>

            <ButtonGroup className="me-2">
                {renderComestibilitate() !== "" &&
                    <Button style={buttonStyle} variant="light" className="border-dark" title={splitCamelCase(comestibilitate).toLocaleLowerCase()}>
                        {renderComestibilitate()}
                    </Button>
                }
                {esteMedicinala &&
                    <Button title="prezintă proprietăți curative" style={buttonStyle} variant="light" className="border-dark">
                        <Medicine></Medicine>
                    </Button>
                }
            </ButtonGroup>

            <ButtonGroup className="me-2">
                {locDeFructificatie && locDeFructificatie?.length !== 0 &&
                    locDeFructificatie?.map((loc, index) =>
                        <Button style={buttonStyle} key={index} variant="light" title={splitCamelCase(loc).toLocaleLowerCase()}
                            className="border-dark">{
                                renderLocDeFructificatie(loc)}
                        </Button>)
                }
            </ButtonGroup>

            <ButtonGroup className="float-end">
                {idSpeciiAsemanatoare && idSpeciiAsemanatoare?.length !== 0 &&
                    idSpeciiAsemanatoare.map(i =>
                        <Button style={buttonStyle}
                            key={i}
                            variant="light"
                            className="border-dark"
                            onClick={() => navigate(`/mushrooms/${i}`)}
                            aria-owns={open ? 'mouse-over-popover' : undefined}
                            aria-haspopup="true"
                            onMouseEnter={handlePopoverOpen}
                            onMouseLeave={handlePopoverClose}>
                            <span className="fw-bold">{i}</span>
                        </Button>

                    )
                }
            </ButtonGroup>
            <Popover
                id="mouse-over-popover"
                sx={{
                    pointerEvents: 'none',
                }}
                open={open}
                anchorEl={anchorEl}
                anchorOrigin={{
                    vertical: 'bottom',
                    horizontal: 'left',
                }}
                transformOrigin={{
                    vertical: 'top',
                    horizontal: 'left',
                }}
                onClose={handlePopoverClose}
                disableRestoreFocus
            >
                <MushroomPopover id={Number(anchorEl?.innerText)}></MushroomPopover>
            </Popover>
        </CardHeader>
    );
};

export default MushroomInfo;
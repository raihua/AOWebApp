import React, { useState } from "react";
import CardV3 from "./CardV3";

const CardList = ({ }) => {
    const [cardData, setCardData] = useState([]);

    React.useEffect(() => {
        fetch("http://localhost:5010/api/ItemsWebAPI")
            .then(res => res.json())
            .then(data => setCardData(data))
            .catch(err => {
                console.log(err);
            })
    }, [])

    return (
        <div className="row">
            {cardData.map((obj) => (
                <CardV3
                    key={obj.itemId}
                    itemId={obj.itemId}
                    itemName={obj.itemName}
                    itemDescription={obj.itemDescription}
                    itemCost={obj.itemCost}
                    itemImage={obj.itemImage}
                />
            )
            )
            }
        </div>
    )
}

export default CardList;
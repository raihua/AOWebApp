import React from "react";

const CardV3 = ({ itemId, itemName, itemDescription, itemCost, itemImage }) => (
        <div className="card" style={{ width: '18rem' }}>
            <img src={itemImage} className="card-img-top" alt={"Image of " + itemName} />
            <div className="card-body">
                <h5 className="card-title">{itemName}</h5>
                <p className="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
                <a href="/" className="btn btn-primary">Go somewhere</a>
            </div>
    </div>
)


export default CardV3;
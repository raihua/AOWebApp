import logo from './logo.svg';
import './App.css';
import Card from './components/Card';
import CardV2 from './components/CardV2';
import CardV3 from './components/CardV3';
import CardList from './components/CardList';
import CardListNew from "./components/CardListNew";
import CardListSearch from "./components/CardListSearch";

function App() {
    return (
        <div className="App container">
            <div className="bg-light py-1 mb-2">
                <h2 className="text-center">Example Application</h2>
            </div>
        {/*    <div className="row justify-content-center">*/}
        {/*        <Card */}
        {/*        itemName="test record1"*/}
        {/*        itemImage="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcShEMAMxnxTtczFKdvDlvlmIk86pkZwvIDpkg&s"*/}
        {/*        />*/}
        {/*        <CardV2*/}
        {/*            itemName="test record2"*/}
        {/*            itemDescription="test record 2 Desc"*/}
        {/*            itemImage="https://upload.wikimedia.org/wikipedia/commons/0/04/So_happy_smiling_cat.jpg"*/}
        {/*            itemCost="10.00"*/}
        {/*        />*/}
        {/*        <CardV3*/}
        {/*            itemName="test record2"*/}
        {/*            itemDescription="test record 2 Desc"*/}
        {/*            itemImage="https://upload.wikimedia.org/wikipedia/commons/0/04/So_happy_smiling_cat.jpg"*/}
        {/*            itemCost="10.00"*/}
        {/*        />*/}
        {/*        <CardList/>*/}
            {/*</div>*/}
            {/*<CardListNew/>*/}
            <CardListSearch/>
        </div>
    )

}

export default App;

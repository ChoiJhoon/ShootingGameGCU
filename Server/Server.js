const express = require('express');
const app = express();
const path = require('path');
const { MongoClient, ObjectId } = require('mongodb');
const url = "mongodb+srv://hun6361:wlgns8873@gcugameshoointing.pkmymvf.mongodb.net/User?appName=gcugameshoointing";
const dbName = 'User';
let db;
let collection;

// MongoDB 클라이언트 생성
const client = new MongoClient(url);

// EJS 템플릿 설정
app.set('view engine', 'ejs');
app.set('views', path.join(__dirname, '/views'));

// JSON 파싱 미들웨어 설정
app.use(express.json());

// POST /scores 엔드포인트 수정
app.post('/scores', async (req, res) => {
    const { id, score, playTime } = req.body;

    let result = {
        cmd: -1,
        message: ''
    };

    try {
        await collection.insertOne({ _id: id, score, playTime });
        result.cmd = 1001;
        result.message = '점수가 신규 등록 되었습니다.';

        console.log('사용자 데이터:', result);
        res.send(result);
    } catch (err) {
        console.error('POST /scores 오류:', err);
        res.status(500).send('서버 오류');
    }
});

// GET /scores/top10 엔드포인트 수정
app.get('/scores/Top10', async (req, res) => {
    try {
        const top10Scores = await collection.find().sort({ score: -1 }).toArray();

        res.json({ scores: top10Scores }); // top10.ejs를 렌더링하며 데이터 전달
    } catch (error) {
        console.error('GET /scores/top10 Error:', error);
        res.status(500).send('Server Error');
    }
});

// GET /scores/:id 엔드포인트 수정
app.get('/scores/:id', async (req, res) => {
    const id = req.params.id;

    try {
        let objectId;
        try {
            objectId = new ObjectId(id);
        } catch (error) {
            res.send({
                cmd: 1103,
                message: '잘못된 id입니다.',
            });
            return;
        }

        let user = await collection.findOne({ _id: objectId });

        if (!user) {
            res.send({
                cmd: 1103,
                message: '잘못된 id입니다.',
            });
        } else {
            res.send({
                cmd: 1102,
                message: '',
                result: user
            });
        }
    } catch (err) {
        console.error('GET /scores/:id 오류:', err);
        res.status(500).send('서버 오류');
    }
});

// 서버 시작 함수
const main = async () => {
    try {
        await client.connect();
        console.log("MongoDB에 연결되었습니다.");
        db = client.db(dbName);
        collection = db.collection('Nick_Score');

        // 서버 시작
        app.listen(3030, () => {
            console.log('서버가 3030 포트에서 실행 중입니다.');
        });
    } catch (err) {
        console.error('MongoDB 연결 실패:', err);
    }
};

main();

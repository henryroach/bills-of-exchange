const fs = require("fs");

let raw = fs.readFileSync("Endorsements.json");
let endorsements = JSON.parse(raw);

const nextEndorsementShouldBe = {};

// Create links between endorsements
for (const endorsement of endorsements) {
  const { BillId, Id } = endorsement;
  const next = nextEndorsementShouldBe[BillId];

  endorsement.PreviousEndorsementId = next || null;

  nextEndorsementShouldBe[BillId] = Id;
}

const toSave = JSON.stringify(endorsements, null, 4);
fs.writeFileSync("Endorsements.json", toSave);

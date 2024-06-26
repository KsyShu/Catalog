'use strict';
const {
  Model
} = require('sequelize');
module.exports = (sequelize, DataTypes) => {
  class ads extends Model {
    /**
     * Helper method for defining associations.
     * This method is not a part of Sequelize lifecycle.
     * The `models/index` file will call this method automatically.
     */
    static associate(models) {
      // define association here
    }
  }
  ads.init({
    name: DataTypes.TEXT,
    tees: DataTypes.INTEGER
  }, {
    sequelize,
    modelName: 'ads',
  });
  return ads;
};
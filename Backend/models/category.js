'use strict';
const {
  Model
} = require('sequelize');
module.exports = (sequelize, DataTypes) => {
  class category extends Model {
    /**
     * Helper method for defining associations.
     * This method is not a part of Sequelize lifecycle.
     * The `models/index` file will call this method automatically.
     */
    static associate(models) {
        //this.belongsTo(models.catalog,{
        //  foreignKey: 'program',
        //})
    }
  }
  category.init({
      name: DataTypes.TEXT,
      price: DataTypes.DECIMAL,
      program: DataTypes.INTEGER
  }, {
    sequelize,
    modelName: 'category',
  });
  return category;
};